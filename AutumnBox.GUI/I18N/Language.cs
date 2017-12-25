/* =============================================================================*\
*
* Filename: Language
* Description: 
*
* Version: 1.0
* Created: 2017/10/30 13:15:49 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Windows;
using System.Xml;

namespace AutumnBox.GUI.I18N
{
    public struct Language
    {
        public string LanguageName { get; private set; }
        public string FileName { get; private set; }
        public string FullUri { get; private set; }
        public Language(string fileName)
        {
            FileName = fileName;
            LanguageName = GetLangName(FileName);
            FullUri = LanguageHelper.Prefix + fileName;
        }
        public ResourceDictionary GetDictionary()
        {
            return LanguageHelper.LoadLangFromResource(this);
        }
        public static string GetLangName(string fileName)
        {
            return LanguageHelper.LoadLangFromResource(fileName)["LanguageName"].ToString();
        }
    }
}
