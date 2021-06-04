using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Converters
{
    public class ImageVisibilityMultiParamConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                int MovesCount = (int)values[0];
                int MinMoves = (int)values[1];
                if (MovesCount == MinMoves && MinMoves > 0)
                {
                    return Visibility.Visible;
                }
            }
            catch (Exception) { }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
