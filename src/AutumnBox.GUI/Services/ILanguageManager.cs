using System;
using System.Collections.Generic;

namespace AutumnBox.GUI.Services
{
    /// <summary>
    /// 语言资源管理器接口
    /// </summary>
    internal interface ILanguageManager
    {
        /// <summary>
        /// 当语言变更时触发
        /// </summary>
        event EventHandler LanguageChanged;
        /// <summary>
        /// 获取或设置当前语言
        /// </summary>
        ILanguage Current { get; set; }
        /// <summary>
        /// 获取已加载的语言
        /// </summary>
        IEnumerable<ILanguage> LoadedLanguages { get; }
        /// <summary>
        /// 加载一个新的语言
        /// </summary>
        /// <param name="language"></param>
        void LoadLanguage(ILanguage language);
        /// <summary>
        /// 默认语言
        /// </summary>
        ILanguage DefaultLanguage { get; }
    }
}
