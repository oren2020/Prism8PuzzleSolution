using System.Collections.Generic;

namespace Business
{
    public class AStar
    {
        #region Members

        private readonly List<Node> aList;

        #endregion

        #region Constructor

        public AStar()
        {
            aList = new List<Node>();
        }

        #endregion

        #region Methods

        private void InsertList(List<Node> list, Node node, int low, int high)
        {
            int gap = high - low;
            if (gap < 0)
            {
                list.Insert(low, node);
                return;
            }

            int mid = gap / 2;
            int idx = low + mid;

            if (node.FScore < list[idx].FScore)
            {
                if (gap == 0)
                {
                    list.Insert(idx, node);
                    return;
                }
                InsertList(list, node, low, idx);
            }
            else if (node.FScore > list[idx].FScore)
            {
                if (gap == 0)
                {
                    list.Insert(idx + 1, node);
                    return;
                }
                InsertList(list, node, idx + 1, high);
            }
            else // equal FScore
            {
                if (node.GScore < list[idx].GScore)
                {
                    if (gap == 0)
                    {
                        list.Insert(idx + 1, node);
                        return;
                    }
                    InsertList(list, node, idx + 1, high);
                }
                else if (node.GScore > list[idx].GScore)
                {
                    if (gap == 0)
                    {
                        list.Insert(idx, node);
                        return;
                    }
                    InsertList(list, node, low, idx);
                }
                else // equal GScore
                {
                    list.Insert(idx, node);
                    return;
                }
            }
        }

        private void InsertList2(List<Node> list, Node node, int low, int high)
        {
            int gap = high - low;
            if (gap < 0)
            {
                list.Insert(low, node);
                return;
            }

            int mid = gap / 2;
            int idx = low + mid;
            if (node.FScore < list[idx].FScore)
            {
                if (gap == 0)
                {
                    list.Insert(idx, node);
                    return;
                }
                InsertList2(list, node, low, idx);
            }
            else if (node.FScore > list[idx].FScore)
            {
                if (gap == 0)
                {
                    list.Insert(idx + 1, node);
                    return;
                }
                InsertList2(list, node, idx + 1, high);
            }
            else // equal FScore
            {
                list.Insert(idx, node);
                return;
            }
        }

        private Node LowestFScore()
        {
            return aList[0];
        }

        public Node AStarSearch(Board MainBoard)
        {
            Board aBoard = new Board(MainBoard.TilesList);
            Node aNode = new Node
            {
                GScore = 0,
                HScore = Manhattan.GetManhattanDistance(MainBoard)
            };
            aNode.FScore = aNode.GScore + aNode.HScore;
            aNode.BoardPathList.Add(aBoard);
            aList.Clear();
            InsertList(aList, aNode, 0, aList.Count - 1);

            while (aList.Count > 0)
            {
                // get lowest FScore
                Node currNode = LowestFScore();
                aList.Remove(currNode);

                // found solution ?
                if (currNode.HScore == 0)
                {
                    return currNode;
                }

                // next available moves
                aBoard = currNode.BoardPathList[^1];
                foreach (Tile t in aBoard.TilesList)
                {
                    if (aBoard.IsAllowSwitch(t.Number))
                    {
                        Board newBoard = new Board(aBoard.TilesList);
                        newBoard.Switch(t.Number);
                        if (currNode.BoardPathList.Count > 1)
                        {
                            bool skeep = false;
                            foreach (Board b in currNode.BoardPathList)
                            {
                                if (newBoard.Equals(b))
                                {
                                    skeep = true;
                                    break;
                                }
                            }
                            if (skeep)
                            {
                                newBoard.Dispose();
                                continue;
                            }
                        }
                        Node newNode = new Node
                        {
                            GScore = currNode.GScore + 1,
                            HScore = Manhattan.GetManhattanDistance(newBoard)
                            //newNode.HScore = Manhattan.GetMisplacedDistance(newBoard);
                        };
                        newNode.FScore = newNode.GScore + newNode.HScore;
                        newNode.UpdateBoardPathList(currNode.BoardPathList);
                        newNode.BoardPathList.Add(newBoard);
                        InsertList(aList, newNode, 0, aList.Count - 1);
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
