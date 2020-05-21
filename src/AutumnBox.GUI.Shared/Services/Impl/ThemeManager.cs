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
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IThemeManager))]
    sealed class ThemeManager : IThemeManager
    {
        [AutoInject] ISettings Settings { get; set; }
        public ThemeMode ThemeMode
        {
            get => Settings.Theme;
            set
            {
                Settings.Theme = value;
                Reload();
            }
        }
        private const int INDEX_OF_THEME = 3;

        private readonly ResourceDictionary LightTheme;
        private readonly ResourceDictionary DarkTheme;
        public ThemeManager()
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
                    Settings.Theme = ThemeMode.Auto;
                    break;
            }
        }
        private bool ShouldUseDarkTheme()
        {
            return false;
        }
    }
}
