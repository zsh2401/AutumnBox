#nullable enable
using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 自适应的国际化信息特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ExtensionI18NTextInfoAttribute : ExtensionInfoAttribute
    {
        /// <summary>
        /// 用于解析形如: zh-CN:你好 的正则解析器
        /// </summary>
        private static readonly Regex KVParseRegex =
                new Regex(@"^(?<langcode>[a-z|A-Z|-]+):(?<text>[\s\S]+)$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// 强制要求覆写键
        /// </summary>
        public abstract override string Key { get; }

        /// <summary>
        /// 值
        /// </summary>
        public sealed override object Value
        {
            get
            {
                var langCode = LakeProvider.Lake.Get<IAppManager>().CurrentLanguageCode.ToLower();
                if (texts.TryGetValue(langCode, out string text))
                {
                    return text;
                }
                else
                {
                    return defaultText;
                }
            }
        }

        /// <summary>
        /// 默认文本值
        /// </summary>
        private readonly string defaultText;

        /// <summary>
        /// 内部维护所有地区代码以及文本内容
        /// </summary>
        private readonly IReadOnlyDictionary<string, string> texts;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="defaultText"></param>
        /// <param name="otherLanguageTexts"></param>
        public ExtensionI18NTextInfoAttribute(string? defaultText,
            params string[] otherLanguageTexts)
        {

            this.defaultText = defaultText ?? "";
            var dictionary = new Dictionary<string, string>();
            otherLanguageTexts.All((text) =>
            {
                var match = KVParseRegex.Match(text);
                if (match.Success)
                {
                    dictionary.Add(match.Result("${langcode}").ToLower(), match.Result("${text}"));
                }
                return true;
            });
            texts = new ReadOnlyDictionary<string, string>(dictionary);
        }
    }
}
