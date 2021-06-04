namespace Business
{
    public class Tile : ViewModelBase
    {
        public static Tile CreateTile(int number, int xAxis, int yAxis)
        {
            return new Tile { Number = number, XAxis = xAxis, YAxis = yAxis };
        }

        private int _number;
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                OnPropertyChanged("Number");
            }
        }

        private int _xAxis;
        public int XAxis
        {
            get { return _xAxis; }
            set
            {
                _xAxis = value;
                OnPropertyChanged("XAxis");
            }
        }

        private int _yAxis;
        public int YAxis
        {
            get { return _yAxis; }
            set
            {
                _yAxis = value;
                OnPropertyChanged("YAxis");
            }
        }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
