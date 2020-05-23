/*

* ==============================================================================
*
* Filename: VisibleIf
* Description: 
*
* Version: 1.0
* Created: 2020/3/16 22:48:27
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace AutumnBox.GUI.Util.UI.Converters
{
    class VisibleIf : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isTrue = parameter as bool? == true;
            var isNotNull = parameter != null;
            if (isNotNull || isTrue)
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
            throw new NotImplementedException();
        }
    }
}
