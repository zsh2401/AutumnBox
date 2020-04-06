using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 语言管理器
    /// </summary>
    public interface ILanguageManager
    {
        /// <summary>
        /// 加载语言
        /// </summary>
        /// <param name="lanCode"></param>
        /// <param name="resourceDictionary"></param>
        void LoadLanguage(string lanCode, IDictionary<string, string> resourceDictionary);
        /// <summary>
        /// 获取当前语言的键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string this[string key] { get; }
    }
}
