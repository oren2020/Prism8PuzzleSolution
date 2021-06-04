using System.Collections.Generic;

namespace Business
{
    public class Node
    {
        #region Private Members


        #endregion

        #region Ctr

        public Node()
        {
            BoardPathList = new List<Board>();
        }

        public Node(List<Board> list)
        {
            BoardPathList = new List<Board>();
            foreach (Board b in list)
            {
                BoardPathList.Add(b);
            }
        }

        #endregion

        #region Public Members

        public int GScore { get; set; }
        public int HScore { get; set; }
        public int FScore { get; set; }
        public List<Board> BoardPathList { get; set; }

        #endregion

        #region Public methods

        public void UpdateBoardPathList(List<Board> pathList)
        {
            foreach (Board b in pathList)
            {
                BoardPathList.Add(b);
            }
        }

        #endregion

    }
}
