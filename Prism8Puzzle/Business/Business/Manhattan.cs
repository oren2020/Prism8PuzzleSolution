using System;
using System.Collections.Generic;

namespace Business
{
    public static class Manhattan
    {
        static Manhattan()
        {
            SolutionTiles();
        }

        private static List<Tile> SolutionTilesList { get; set; }

        public static int GetManhattanDistance(Board board)
        {
            int manhattanDistance = 0;
            foreach (Tile t in board.TilesList)
            {
                if (t.Number != 0)
                {
                    Tile tile = GetTileByNumber(t.Number);
                    manhattanDistance += Math.Abs(t.XAxis - tile.XAxis) + Math.Abs(t.YAxis - tile.YAxis);
                    tile.Dispose();
                }
            }
            return manhattanDistance;
        }

        public static int GetMisplacedDistance(Board board)
        {
            int misplacedDistance = 0;
            foreach (Tile t in board.TilesList)
            {
                if (t.Number != 0)
                {
                    Tile tile = GetTileByNumber(t.Number);
                    misplacedDistance += ((t.XAxis != tile.XAxis) || (t.YAxis != tile.YAxis)) ? 1 : 0;
                    tile.Dispose();
                }
            }
            return misplacedDistance;
        }

        private static void SolutionTiles()
        {
            SolutionTilesList = new List<Tile>
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
        }

        private static Tile GetTileByNumber(int num)
        {
            int idx = num == 0 ? 8 : num - 1;
            return Tile.CreateTile(SolutionTilesList[idx].Number, SolutionTilesList[idx].XAxis, SolutionTilesList[idx].YAxis);
        }
    }
}
