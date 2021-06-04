using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Converters
{
    public class ImageSourceMultiValueConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            BitmapImage image = null;
            string imgFile = "thumbUp.png";
            try
            {
                int movesCount = (int)values[0];
                int minMoves = (int)values[1];
                bool isGmeOver = (bool)values[2];
                if (movesCount > minMoves && isGmeOver)
                {
                    imgFile = "okHand.png";
                }

                image = new BitmapImage(new Uri(GetImagePath(imgFile)));
            }
            catch (Exception)
            { 
            }
            return image;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string GetImagePath(string imgFile)
        {
            string imagName = imgFile;
            return new BitmapImage(new Uri(string.Format("pack://application:,,,/Library;component/Images/{0}", imagName))).ToString();
        }
    }
}
