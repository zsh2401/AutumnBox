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
using AutumnBox.GUI.Properties;
using System;
using System.Collections.Generic;

namespace AutumnBox.GUI.I18N
{
    /// <summary>
    /// 界面的语言切换帮助类
    /// </summary>
    //internal static class LanguageHelper
    //{
    //    public static event EventHandler LanguageChanged;
    //    public static readonly List<Language> Langs;
    //    public const string Path = "pack://application:,,,/AutumnBox.GUI;component/I18N/Langs/";
    //    static LanguageHelper()
    //    {
    //        Langs = new List<Language>(){
    //            new Language("zh-CN"),
    //            new Language("en-US")
    //        };
    //    }
    //    public static bool SystemLanguageIsChinese
    //    {
    //        get
    //        {
    //            return System.Threading.Thread.CurrentThread.CurrentCulture.Name == "zh-CN";
    //        }
    //    }
    //    public static void SetLanguageByEnvironment()
    //    {
    //        switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
    //        {
    //            case "zh-TW":
    //            case "zh-CN":
    //            case "zh-SG":
    //            case "zh-HK":
    //                SetLanguage("zh-CN");
    //                break;
    //            case "ru-RU":
    //            case "ru":
    //            case "uk":
    //            case "uk-UA":
    //                SetLanguage("ru-RU");
    //                break;
    //            default:
    //                SetLanguage("en-US");
    //                break;
    //        }
    //    }
    //    public static int FindIndex(string langCode)
    //    {
    //        return Langs.FindIndex((lang) => { return lang.LanguageCode == langCode; });
    //    }
    //    private static void SaveLangSetting()
    //    {
    //        Settings.Default.Language = App.Current.Resources["LanguageCode"].ToString();
    //        Settings.Default.Save();
    //    }
    //    public static void SetLanguage(string languageCode)
    //    {
    //        try
    //        {
    //            var lang = Langs.Find((language) => language.LanguageCode == languageCode);
    //            App.Current.Resources.MergedDictionaries[0] = lang.Resources;
    //            SaveLangSetting();
    //            LanguageChanged?.Invoke(new object(), new EventArgs());
    //        }
    //        catch (Exception)
    //        {
    //            SetLanguage("zh-CN");
    //        }
    //    }
    //    public static void SetLanguage(Language language)
    //    {
    //        SetLanguage(language.LanguageCode);
    //    }
    //}
}
