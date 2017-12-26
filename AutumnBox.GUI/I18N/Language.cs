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
#pragma warning disable CS0659 // 类型重写 Object.Equals(object o)，但不重写 Object.GetHashCode()
    public sealed class Language
#pragma warning restore CS0659 // 类型重写 Object.Equals(object o)，但不重写 Object.GetHashCode()
    {

        private const string pathPrefix = "pack://application:,,,/AutumnBox;component/I18N/Langs/";
        public ResourceDictionary Resources
        {
            get
            {
                if (_resources == null)
                {
                    Reload();
                }
                return _resources;
            }
        }
        private ResourceDictionary _resources;
        public string LanguageName
        {
            get
            {
                return Resources["LanguageName"].ToString();
            }
        }
        public string FileName { get; private set; }
        public Language(string fileName)
        {
            this.FileName = fileName;
        }
        public void Reload()
        {
            _resources = new ResourceDictionary { Source = new Uri(pathPrefix + FileName) };
        }
        public override bool Equals(object obj)
        {
            if (obj is Language)
            {
                return this.FileName == ((Language)obj).FileName;
            }
            return base.Equals(obj);
        }
    }
}
