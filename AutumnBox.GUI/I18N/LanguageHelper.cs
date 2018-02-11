/* =============================================================================*\
*
* Filename: LanguageHelper.cs
* Description: 
*
* Version: 1.0
* Created: 9/23/2017 21:21:10(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Cfg;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.I18N
{
    [LogProperty(TAG = "Language Manage")]
    /// <summary>
    /// 界面的语言切换帮助类
    /// </summary>
    public static class LanguageHelper
    {
        public static event EventHandler LanguageChanged;
        public static readonly List<Language> Langs;
        public static readonly string Prefix = "pack://application:,,,/AutumnBox;component/I18N/Langs/";
        static LanguageHelper()
        {
            Langs = new List<Language>(){
                new Language("zh-CN.xaml"),
                new Language("en-US.xaml")
            };
        }
        private const string zh_cn = "zh-CN";
        private const string en_us = "en-US";
        public static void SetLanguageByEnvironment() {

            var lang_name = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
            Logger.D($"this system language: {lang_name}");
            switch (lang_name) {
                case zh_cn:
                    LoadLanguage("简体中文");
                    break;
                case en_us:
                default:
                    LoadLanguage("English");
                    break;
            }
        }
        public static int GetLangIndex(string langName)
        {
            return Langs.FindIndex((lang) => { return lang.LanguageName == langName; });
        }
        public static void LoadLanguage(Language lang)
        {
            App.Current.Resources.MergedDictionaries[0] = lang.Resources;
            SaveLangSetting();
            LanguageChanged?.Invoke(new object(), new EventArgs());
        }
        public static void LoadLanguage(string langName) {
            App.Current.Resources.MergedDictionaries[0] = Langs[GetLangIndex(langName)].Resources;
            SaveLangSetting();
            LanguageChanged?.Invoke(new object(), new EventArgs());
        }
        public static void LoadLanguageByFileName(string fileName) {
            int index = Langs.FindIndex((lang) =>
            {
                return lang.FileName == fileName;
            });
            LoadLanguage(Langs[index]);
        }
        private static void SaveLangSetting() {
            Config.Lang = Langs[GetLangIndex(App.Current.Resources["LanguageName"].ToString())].FileName;
        }
    }
}
