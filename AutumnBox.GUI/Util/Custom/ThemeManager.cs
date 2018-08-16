/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/13 17:56:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Properties;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.Util.Custom
{
    internal interface ITheme
    {
        string Name { get; }
        ResourceDictionary Resource { get; }
    }
    internal class RandomTheme : ITheme
    {
        public string Name => "随机-Random";
        private static readonly Random ran = new Random();
        public ResourceDictionary Resource => Next();
        private ResourceDictionary Next()
        {
            var themes = ThemeManager.Themes;
            int ranIndex = ran.Next(1, themes.Length);
            return themes[ranIndex].Resource;
        }
    }
    internal class FileTheme : ITheme
    {
        public string Name { get { return Resource["ThemeName"].ToString(); } }
        public ResourceDictionary Resource => resouce;
        private readonly ResourceDictionary resouce;
        public FileTheme(string fileName)
        {
            resouce = new ResourceDictionary() { Source = new Uri(ThemeManager.Path + fileName) };
        }
    }
    internal static class ThemeManager
    {
        public static event EventHandler ThemeChanged;
        public static ITheme[] Themes => themes.ToArray();
        private static readonly List<ITheme> themes;
        public const string Path = "pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/";
        static ThemeManager()
        {
            themes = new List<ITheme>() {
                new RandomTheme(),
                new FileTheme("LightTheme.xaml"),
                new FileTheme("NightTheme.xaml"),
                new FileTheme("AutumnTheme.xaml"),
                new FileTheme("SpringTheme.xaml"),
                new FileTheme("DreamTheme.xaml"),
                new FileTheme("PinkTheme.xaml"),
                new FileTheme("PurpleTheme.xaml")
            };
        }
        public static void LoadFromSetting()
        {
            ChangeTheme(Settings.Default.Theme);
        }
        static bool usingRandomTheme = false;
        public static void ChangeTheme(ITheme theme)
        {
            usingRandomTheme = theme is RandomTheme;
            App.Current.Resources.MergedDictionaries[1] = theme.Resource;
            ThemeChanged?.Invoke(new object(), new EventArgs());
            Settings.Default.Theme = theme.Name;
            Settings.Default.Save();
        }
        public static void ChangeTheme(string themeName)
        {
            var theme = themes.Find((_theme) => { return _theme.Name == themeName; });
            ChangeTheme(theme);
        }
        public static int GetCrtIndex()
        {
            if (usingRandomTheme) return 0;
            else
            {
                int index;
                index = themes.FindIndex((theme) =>
                {
                    return theme.Name == App.Current.Resources["ThemeName"].ToString();
                });
                return index;
            }
        }
    }
}
