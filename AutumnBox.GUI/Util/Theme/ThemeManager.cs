/*

* ==============================================================================
*
* Filename: ThemeManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/14 15:37:36
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.Properties;
using System;
using System.Windows;

namespace AutumnBox.GUI.Util.Theme
{
    class ThemeManager : IThemeManager
    {
        public ThemeMode ThemeMode
        {
            get => (ThemeMode)Settings.Default.Theme;
            set
            {
                Settings.Default.Theme = (int)value;
                Reload();
                Settings.Default.Save();
            }
        }
        private const int INDEX_OF_THEME = 1;

        private readonly ResourceDictionary LightTheme;
        private readonly ResourceDictionary DarkTheme;
        public static IThemeManager Instance { get; }
        static ThemeManager()
        {
            Instance = new ThemeManager();
        }
        private ThemeManager()
        {
            LightTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/ThemeLight.xaml")
            };
            DarkTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/ThemeDark.xaml")
            };
        }
        public void Reload()
        {
            switch (ThemeMode)
            {
                case ThemeMode.Light:
                    App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = LightTheme;
                    break;
                case ThemeMode.Dark:
                    App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = DarkTheme;
                    break;
                default:
                case ThemeMode.Auto:
                    if (ShouldUseDarkTheme())
                    {
                        App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = LightTheme;
                    }
                    else
                    {
                        App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = LightTheme;
                    }
                    Settings.Default.Theme = (int)ThemeMode.Auto;
                    Settings.Default.Save();
                    break;
            }
        }
        private bool ShouldUseDarkTheme()
        {
            return false;
        }
    }
}
