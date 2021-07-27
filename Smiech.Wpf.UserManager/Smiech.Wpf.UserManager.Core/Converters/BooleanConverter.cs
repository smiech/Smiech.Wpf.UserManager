using System;
using System.Globalization;
using System.Windows.Data;

namespace Smiech.Wpf.UserManager.Core.Converters
{
    public class BooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool) && (bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b) && b;
        }
    }
}
