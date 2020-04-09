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

namespace AutumnBox.GUI.Resources.Languages
{
    /// <summary>
    /// 快速读取语言的帮助类
    /// </summary>
    internal static class Lang
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Get(string key, bool threadSafe = true)
        {
            if (threadSafe)
            {
                return App.Current.Dispatcher.Invoke(() =>
                {
                    return (string)App.Current.Resources[key];
                });
            }
            else
            {
                return (string)App.Current.Resources[key];
            }
        }
        /// <summary>
        /// 安全地获取，当找不到对应文本时，将返回键
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Safe(string key, bool threadSafe = true)
        {
            if (threadSafe)
            {
                return App.Current.Dispatcher.Invoke(() =>
               {
                   return App.Current.Resources[key] as string;
               }) ?? key;
            }
            else
            {
                return App.Current.Resources[key] as string ?? key;
            }
        }
    }
}
