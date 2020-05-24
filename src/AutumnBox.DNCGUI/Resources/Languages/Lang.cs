/*

* ==============================================================================
*
* Filename: Lang
* Description: 
*
* Version: 1.0
* Created: 2020/3/15 21:09:37
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System.Linq;
using System.Collections.Generic;
using System.Windows;
using System;

namespace AutumnBox.GUI.Resources.Languages
{
    /// <summary>
    /// 快速读取语言的帮助类
    /// </summary>
    internal static class Lang
    {
        public static List<(string name, string code, string resourceUri)> Langs { get; }

        static Lang()
        {
            Langs = new List<(string, string, string)>() {
                ( "简体中文","zh-CN","pack://application:,,,/AutumnBox.GUI;component/Resources/Languages/zh-CN.xaml"),
                ("English","en-US","pack://application:,,,/AutumnBox.GUI;component/Resources/Languages/en-US.xaml"),
            };
        }

        public static bool FileCheck()
        {
            IEnumerable<ResourceDictionary> langResourceDictionaries =
                from langInfo in Langs
                select new ResourceDictionary() { Source = new System.Uri(langInfo.Item3) };

            var baseLang = langResourceDictionaries.FirstOrDefault();
            bool allOk = true;

            for (int i = 1; i < langResourceDictionaries.Count(); i++)
            {
                allOk = DiffAndPrint(baseLang, langResourceDictionaries.ElementAt(i));
            }
            return allOk;
        }
        private static bool DiffAndPrint(ResourceDictionary baseLang, ResourceDictionary other)
        {
            var keysArray = new object[baseLang.Keys.Count];
            baseLang.Keys.CopyTo(keysArray, 0);
            var otherKeysArray = new object[other.Keys.Count];
            other.Keys.CopyTo(otherKeysArray, 0);

            var missing = from key in keysArray
                          where !otherKeysArray.Contains(key)
                          select key;

            var extra = from key in otherKeysArray
                        where !keysArray.Contains(key)
                        select key;

            missing.All((k) =>
            {
                Console.WriteLine($"missing: {k}");
                return true;
            });

            extra.All((k) =>
            {
                Console.WriteLine($"extra: {k}");
                return true;
            });

            return !(missing.Any() || extra.Any());
        }
    }
}
