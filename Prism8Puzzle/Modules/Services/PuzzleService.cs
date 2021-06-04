using Business;
using Prism8Puzzle.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Services
{
    public class PuzzleService : IPuzzleService
    {
        #region Members

        readonly Random rand = new Random();
        private readonly AStar aStar = new AStar();
        public Board Board { get; set; }

        #endregion

        #region Public methods

        public void GetRandomPuzzleAsync(EventHandler<TilesServiceResult> callback)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (o, e) =>
            {
                e.Result = GetShuffledSolvable();
            };
            backgroundWorker.RunWorkerCompleted += (o, e) =>
            {
                callback.Invoke(this, new TilesServiceResult((IList<Tile>)e.Result));
            };
            backgroundWorker.RunWorkerAsync();
        }

        public void GetSolutionPuzzleAsync(EventHandler<TilesServiceResult> callback)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (o, e) =>
            {
                e.Result = SolutionTiles();
            };
            backgroundWorker.RunWorkerCompleted += (o, e) =>
            {
                callback.Invoke(this, new TilesServiceResult((IList<Tile>)e.Result));
            };
            backgroundWorker.RunWorkerAsync();
        }

        public void GetSolvedPuzzleAsync(EventHandler<NodeServiceResult> callback)
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (o, e) =>
            {
                //Thread.Sleep(1000);
                e.Result = GetSolvedNode();
            };
            backgroundWorker.RunWorkerCompleted += (o, e) =>
            {
                callback.Invoke(this, new NodeServiceResult((Node)e.Result));
            };
            backgroundWorker.RunWorkerAsync();
        }

        #endregion

        #region Private methods

        private IList<Tile> GetShuffledSolvable()
        {
            IList<Tile> tiles;
            bool isSolvable;
            do
            {
                tiles = GetShuffled();
                isSolvable = CheckSolvable(tiles);
            } while (!isSolvable);

            return tiles;
        }

        private IList<Tile> SolutionTiles()
        {
            List<Tile> tiles = new List<Tile>
            {
               Tile.CreateTile(1, 0, 0),
                Tile.CreateTile(2, 1, 0),
                Tile.CreateTile(3, 2, 0),
                Tile.CreateTile(4, 0, 1),
                Tile.CreateTile(5, 1, 1),
                Tile.CreateTile(6, 2, 1),
                Tile.CreateTile(7, 0, 2),
                Tile.CreateTile(8, 1, 2),
                Tile.CreateTile(0, 2, 2)
            };
            return tiles;
        }

        private IList<Tile> GetShuffled()
        {
            List<Tile> tiles = new List<Tile>(9);
            int xAx = 0;
            int yAx = 0;
            int listSize = 0;

            for (int i = 0; i < 9; i++)
            {
                int num = rand.Next(0, 9);
                if (IsNumberExist(num, listSize, tiles))
                {
                    i--;
                }
                else
                {
                    tiles.Add(Tile.CreateTile(num, xAx++, yAx));
                    listSize++;
                    if (xAx > 2)
                    {
                        xAx = 0;
                        yAx++;
                    }
                }
            }

            return tiles;
        }

        private bool CheckSolvable(IList<Tile> tiles)
        {
            int inversions = 0;

            for (int i = 0; i < tiles.Count; i++)
            {
                if (tiles[i].Number > 0)
                {
                    for (int j = i + 1; j < tiles.Count; j++)
                    {
                        if (tiles[j].Number > 0)
                        {
                            if (tiles[j].Number > tiles[i].Number)
                            {
                                inversions++;
                            }
                        }
                    }
                }
            }

            if (inversions % 2 == 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsNumberExist(int num, int listSize, List<Tile> tiles)
        {
            for (int x = 1; x <= listSize; x++)
            {
                if (tiles[x - 1].Number == num)
                {
                    return true;
                }
            }
            return false;
        }

        private Node GetSolvedNode()
        {
            return aStar.AStarSearch(Board);
        }

        #endregion
    }
}
