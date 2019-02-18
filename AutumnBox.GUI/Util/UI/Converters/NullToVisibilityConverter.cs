using AutumnBox.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Linq;
namespace AutumnBox.GUI.Util.UI.Converters
{
    [ValueConversion(typeof(object), typeof(Visibility), ParameterType = typeof(NullToVisibilityConverterParameter))]
    class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SLogger.Info(this, $"p:{parameter} v:{(value ?? "null")}");
            if (parameter is NullToVisibilityConverterParameter.VisibleIf_IsNull)
            {
                return VisibleIf_IsNull(value);
            }
            else
            {
                return VisibleIf_IsNotNull(value);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null || parameter is NullToVisibilityConverterParameter.VisibleIf_IsNotNull)
            {
                return value == null ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return value != null ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility VisibleIf_IsNull(object v)
        {
            return v == null ? Visibility.Visible : Visibility.Collapsed;
        }
        private Visibility VisibleIf_IsNotNull(object v)
        {
            return v != null ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
