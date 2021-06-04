using System;
using System.Globalization;
using System.Windows.Data;

namespace Converters
{
    public class HintTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is int @int && @int > 0 ? value.ToString() : "?";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
