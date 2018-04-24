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
using System.Collections.Generic;
using System.Windows;

namespace AutumnBox.GUI.I18N
{
    internal sealed class Language : IEquatable<Language>
    {
        public ResourceDictionary Resources
        {
            get
            {
                return _resources;
            }
        }
        private ResourceDictionary _resources;
        public string LanguageCode
        {
            get
            {
                return Resources["LanguageCode"].ToString();
            }
        }
        public string LanguageName
        {
            get
            {
                return Resources["LanguageName"].ToString();
            }
        }
        public string FileName
        {
            get
            {
                return string.Format("{0}/{1}.xaml", LanguageHelper.Path, LanguageCode);
            }
        }
        public Language(string languageCode)
        {
            _resources = new ResourceDictionary { Source = new Uri(LanguageHelper.Path + languageCode + ".xaml") };
        }
        public override bool Equals(object obj)
        {
            return Equals(obj as Language);
        }
        public bool Equals(Language other)
        {
            return other != null && LanguageCode == other.LanguageCode;
        }
        public override int GetHashCode()
        {
            var hashCode = -1597780829;
            hashCode = hashCode * -1521134295 + EqualityComparer<ResourceDictionary>.Default.GetHashCode(Resources);
            hashCode = hashCode * -1521134295 + EqualityComparer<ResourceDictionary>.Default.GetHashCode(_resources);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LanguageCode);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(LanguageName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(FileName);
            return hashCode;
        }
    }
}
