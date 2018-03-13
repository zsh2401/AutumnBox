/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/13 17:56:01 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Helper
{
    internal class Theme
    {
        public string Name { get { return Resource["ThemeName"].ToString(); } }
        public ResourceDictionary Resource { get; }
        public Theme(string fileName)
        {
            Resource = new ResourceDictionary { Source = new Uri(ThemeHelper.Path + fileName) };
        }
    }
    internal static class ThemeHelper
    {
        public static readonly Theme[] Themes;
        public const string Path = "pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/";
        static ThemeHelper()
        {
            Themes = new Theme[] {
                new Theme("LightTheme.xaml"),
                new Theme("NightTheme.xaml")
            };
        }
        public static void ChangeTheme(Theme theme)
        {
            App.Current.Resources.MergedDictionaries[1] = theme.Resource;
        }
    }
}
