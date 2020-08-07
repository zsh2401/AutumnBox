using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AutumnBox.GUI.Util.UI.Converters
{
    class VisibleIfNot : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = parameter as bool? == true;
            var isNotNull = parameter != null;
            if (isNotNull || isTrue)
            {
                return Visibility.Collapsed;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
