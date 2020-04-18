using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ClassTextAttribute : Attribute
    {
        /// <summary>
        /// 获取键
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// 根据当前线程语言环境获得值
        /// </summary>
        public string Value
        {
            get
            {

            }
        }
        private readonly IReadOnlyDictionary<string, string> translatedTexts;
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

        }
    }
}
