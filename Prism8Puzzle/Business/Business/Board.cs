using System;
using System.Collections.ObjectModel;

namespace Business
{
    public class Board : ViewModelBase
    {
        #region Private Members

        private ObservableCollection<Tile> _tilesList;

        #endregion

        #region Ctr

        public Board()
        {
        }
        public Board(ObservableCollection<Tile> tilesList)
        {
            _tilesList = new ObservableCollection<Tile>();

            foreach (Tile t in tilesList)
            {
                _tilesList.Add(Tile.CreateTile(t.Number, t.XAxis, t.YAxis));
            }
        }

        #endregion

        #region Public Members

        public ObservableCollection<Tile> TilesList
        {
            get { return _tilesList; }
            set
            {
                _tilesList = value;
                OnPropertyChanged("TilesList");
            }
        }

        #endregion

        #region Private Methods

        private Tile GetTileByNumber(int num)
        {
            int i = 0;
            foreach (Tile t in TilesList)
            {
                if (t.Number == num)
                {
                    return Tile.CreateTile(TilesList[i].Number, TilesList[i].XAxis, TilesList[i].YAxis);
                }
                i++;
            }
            return null;
        }

        private int GetIndexByNumber(int num)
        {
            int idx = 0;
            int i = 0;
            foreach (Tile t in TilesList)
            {
                if (t.Number == num)
                {
                    idx = i;
                }
                i++;
            }
            return idx;
        }

        #endregion

        #region Public Methods

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Board p = (Board)obj;
            int c = 0;
            foreach (Tile t in TilesList)
            {
                if (t.Number != p.TilesList[c++].Number)
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public void Switch(int n1, int n2 = 0)
        {
            if (IsAllowSwitch(n1, n2))
            {
                int res1 = GetIndexByNumber(n1);
                int res2 = GetIndexByNumber(n2);
                int n = TilesList[res1].Number;
                TilesList[res1].Number = TilesList[res2].Number;
                TilesList[res2].Number = n;
            }
        }

        public bool IsAllowSwitch(int n1, int n2 = 0)
        {
            bool ret = false;
            Tile t1 = GetTileByNumber(n1);
            Tile t2 = GetTileByNumber(n2);
            if (
                (Math.Abs(t1.XAxis - t2.XAxis) == 1 && t1.YAxis == t2.YAxis)
                ||
                (Math.Abs(t1.YAxis - t2.YAxis) == 1 && t1.XAxis == t2.XAxis)
                )
            {
                ret = true;
            }
            t1.Dispose();
            t2.Dispose();
            return ret;
        }

        protected override void OnDispose()
        {
            TilesList.Clear();
        }

        #endregion
    }
}
