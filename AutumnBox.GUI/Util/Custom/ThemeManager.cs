/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 15:41:22 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.Properties;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.Util.Custom
{
    class ThemeManager : IThemeManager
    {
        private const int INDEX_OF_THEME = 2;
        private const string FILE_PATH = "pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/";
        private const string THEME_NAME_KEY = "ThemeName";
        public static ThemeManager Instance { get; private set; }
        static ThemeManager()
        {
            Instance = new ThemeManager();
        }

        public IEnumerable<ITheme> Themes => themes;

        public ITheme Current
        {
            get => current;
            set
            {
                current = value ?? throw new ArgumentNullException();
                Apply(current);
            }
        }
        private ITheme current;

        private List<ITheme> themes;
        private ThemeManager()
        {
            Load();
        }
        private void Load()
        {
            themes = new List<ITheme>() {
                ThemeImpl.LoadFrom("Autumn.xaml"),
            };
        }

        public void ApplyBySetting()
        {
            var settingTheme = Settings.Default.Theme;
            var findingResult = themes.Find(_the => _the.ThemeName == settingTheme);
            Current = findingResult;
        }

        private void Apply(ITheme theme)
        {
            if (theme == null)
            {
                return;
            }
            App.Current.Resources.MergedDictionaries[INDEX_OF_THEME] = theme.Resource;
            current = theme;
        }

        private class ThemeImpl : ITheme
        {
            public string ThemeName => Resource[THEME_NAME_KEY].ToString();

            public ResourceDictionary Resource { get; private set; }

            public static ThemeImpl LoadFrom(string filename)
            {
                var resouceDict = new ResourceDictionary { Source = new Uri(FILE_PATH + filename) };
                return new ThemeImpl()
                {
                    Resource = resouceDict,
                };
            }
        }
        private class RandomTheme : ITheme
        {
            public string ThemeName => "Random-随机";

            public ResourceDictionary Resource => throw new NotImplementedException();
            private ResourceDictionary current;
            public void Random() { }
        }
    }
}
