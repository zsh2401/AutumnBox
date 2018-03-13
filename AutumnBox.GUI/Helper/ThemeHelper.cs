/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/13 17:56:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Properties;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.Helper
{
    internal abstract class Theme
    {
        public abstract string Name { get; }
        public abstract ResourceDictionary Resource { get; }
    }
    internal class RandomTheme : Theme
    {
        public override string Name => $"随机-Random";
        private static readonly Random ran = new Random();
        public override ResourceDictionary Resource => resouce;
        private ResourceDictionary resouce;
        public void Next()
        {
            var themes = ThemeHelper.Themes;
            int ranIndex = ran.Next(1, themes.Length);
            Logger.Debug(this,ranIndex);
            resouce = themes[ranIndex].Resource;
        }
    }
    internal class FileTheme : Theme
    {
        public override string Name { get { return Resource["ThemeName"].ToString(); } }
        public override ResourceDictionary Resource => resouce;
        private readonly ResourceDictionary resouce;
        public FileTheme(string fileName)
        {
            resouce = new ResourceDictionary() { Source = new Uri(ThemeHelper.Path + fileName) };
        }
    }
    internal static class ThemeHelper
    {
        public static Theme[] Themes => themes.ToArray();
        private static readonly List<Theme> themes;
        public const string Path = "pack://application:,,,/AutumnBox.GUI;component/Resources/Themes/";
        static ThemeHelper()
        {
            themes = new List<Theme>() {
                new RandomTheme(),
                new FileTheme("LightTheme.xaml"),
                new FileTheme("NightTheme.xaml")
            };
            (themes[0] as RandomTheme).Next();
        }
        public static void LoadFromSetting()
        {
            ChangeTheme(Settings.Default.Theme);
        }
        static bool usingRandomTheme = false;
        public static void ChangeTheme(Theme theme)
        {
            usingRandomTheme = theme is RandomTheme;
            (theme as RandomTheme)?.Next();
            App.Current.Resources.MergedDictionaries[1] = theme.Resource;
            Settings.Default.Theme = theme.Name;
            Logger.Info("ThemeHelper",$"Theme setting saved,value {theme.Name}");
        }
        public static void ChangeTheme(string themeName)
        {
            
            var theme = themes.Find((_theme) => { return _theme.Name == themeName; });
            Logger.Debug("ThemeHelper",$"Theme finded {theme.Name}");
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
