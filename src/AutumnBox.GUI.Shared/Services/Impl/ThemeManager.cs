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
using AutumnBox.GUI.Util;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using System;
using System.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IThemeManager))]
    sealed class ThemeManager : IThemeManager
    {
        public ThemeMode ThemeMode
        {
            get => _themeMode; set
            {
                _themeMode = value;
                Reload();
            }
        }
        ThemeMode _themeMode;

        private const int INDEX_OF_THEME = 3;

        private readonly ResourceDictionary LightTheme;
        private readonly ResourceDictionary DarkTheme;
        public ThemeManager(ISettings settings)
        {
            LightTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/ThemeLight.xaml")
            };
            DarkTheme = new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/ThemeDark.xaml")
            };
            _themeMode = settings.Theme;
        }
        public void Reload()
        {
            var themeDictionary = ThemeMode switch
            {
                ThemeMode.Light => LightTheme,
                ThemeMode.Dark => DarkTheme,
                _ => ShouldUseDarkTheme() ? DarkTheme : LightTheme
            };
            ApplyTheme(themeDictionary);
        }
        private void ApplyTheme(ResourceDictionary themeDictionary)
        {
            App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = themeDictionary;
        }
        ~ThemeManager()
        {
            this.GetComponent<ISettings>().Theme = ThemeMode;
        }
        private bool ShouldUseDarkTheme()
        {
            int hour = DateTime.Now.Hour;
            return (hour > 18 || hour < 6);
        }
    }
}
