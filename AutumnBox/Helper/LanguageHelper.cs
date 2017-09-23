using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.Helper
{
    public static class LanguageHelper
    {
        public enum LanguageType
        {
            zh_CN,
            en_US,
        }
        public static LanguageType CurrentLanguage { get; private set; }
        public static void ChangeLanuage(LanguageType lang)
        {
            string _lang = lang.ToString().Replace('_', '-');
            if (App.Current.Resources["LanguageName"].ToString() != lang.ToString())
                Application.Current.Resources.Source = new Uri($@"Lang\{_lang}.xaml", UriKind.Relative);
        }
    }
}
