using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AutumnBox.GUI.Util.UI.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility), ParameterType = typeof(BoolToVisibilityConverterParameter))]
    class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is null || parameter is BoolToVisibilityConverterParameter.VisibleIf_IsTrue)
            {
                return VisibilityIfIsTrue((bool)value);
            }
            else
            {
                return VisibilityIfIsFalse((bool)value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is null || parameter is BoolToVisibilityConverterParameter.VisibleIf_IsTrue)
            {
                return value is Visibility.Visible;
            }
            else
            {
                return !(value is Visibility.Visible);
            }
        }

        private Visibility VisibilityIfIsTrue(bool v)
        {
            return v ? Visibility.Visible : Visibility.Collapsed;
        }
        private Visibility VisibilityIfIsFalse(bool v)
        {
            return !v ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
