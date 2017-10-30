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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AutumnBox.GUI.I18N
{
    public struct Language
    {
        public string LanguageName { get; set; }
        public string FilePath { get; set; }
        public Language(string filePath)
        {
            FilePath = filePath;
            LanguageName = GetLangName(FilePath);
        }
        public static string GetLangName(string filePath)
        {
            using (var reader = XmlReader.Create($"pack://application:,,,/AutumnBox.Res;Component/Lang{filePath}"))
            {
                return reader["LanguageName"].ToString();
            }
        }
    }
}
