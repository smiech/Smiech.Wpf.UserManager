using System;
using System.Globalization;
using System.Windows.Data;

namespace Smiech.Wpf.UserManager.Core.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b) && !b;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool b) && !b;
        }
    }
}
