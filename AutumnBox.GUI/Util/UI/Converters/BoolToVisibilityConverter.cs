using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AutumnBox.GUI.Util.UI.Converters
{
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is true)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
