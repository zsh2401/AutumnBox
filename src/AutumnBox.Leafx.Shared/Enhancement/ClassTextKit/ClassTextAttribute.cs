#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// 类文本特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ClassTextAttribute : Attribute
    {
        /// <summary>
        /// 获取键
        /// </summary>
        public virtual string Key { get; }

        /// <summary>
        /// 根据当前线程语言环境获得值
        /// </summary>
        public virtual string Value
        {
            get
            {
                return GetText();
            }
        }

        /// <summary>
        /// 获取文本
        /// </summary>
        /// <param name="_regionCode">指定的区域码</param>
        /// <returns>对应区域的文本</returns>
        public string GetText(string? _regionCode = null)
        {
            var regionCode = _regionCode ?? Thread.CurrentThread.CurrentCulture.Name;
            if (translatedTexts.TryGetValue(regionCode.ToLower(), out string value))
            {
                return value;
            }
            else
            {
                return defaultText;
            }
        }

        /// <summary>
        /// 内部维护的仅读字典,存储着翻译文本
        /// </summary>
        private readonly IReadOnlyDictionary<string, string> translatedTexts;

        /// <summary>
        /// 默认文本
        /// </summary>
        private readonly string defaultText;

        /// <summary>
        /// 类文本特性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultText">默认文本,当所有翻译文本都不匹配时使用</param>
        /// <param name="translatedTexts">
        /// 翻译文本:
        /// 形如   "zh-CN:你好","en-UK:Hello","en-US:Hello"
        /// </param>
        /// <exception cref="ArgumentException">参数为空</exception>
        public ClassTextAttribute(string key, string defaultText, params string[] translatedTexts)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            if (string.IsNullOrEmpty(defaultText))
            {
                throw new ArgumentException("message", nameof(defaultText));
            }

            if (translatedTexts is null)
            {
                throw new ArgumentNullException(nameof(translatedTexts));
            }

            this.defaultText = defaultText;
            this.Key = key;

            this.translatedTexts = ReadTranslatedText(translatedTexts);
        }

        /// <summary>
        /// 用于解析的正则引擎
        /// </summary>
        private static readonly Regex RegionTextParseEngine =
                new Regex(@"^(?<regionCode>[a-z|A-Z|-]+):(?<text>[\s\S]+)$", RegexOptions.Compiled | RegexOptions.Multiline);

        /// <summary>
        /// 读取文本
        /// </summary>
        /// <param name="translatedTexts"></param>
        /// <returns></returns>
        private static IReadOnlyDictionary<string, string> ReadTranslatedText(string[] translatedTexts)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var t in translatedTexts)
            {
                var match = RegionTextParseEngine.Match(t);
                if (match.Success)
                {
                    result.Add(match.Result("${regionCode}").ToLower(), match.Result("${text}"));
                }
            }
            return new ReadOnlyDictionary<string, string>(result);
        }
    }
}
