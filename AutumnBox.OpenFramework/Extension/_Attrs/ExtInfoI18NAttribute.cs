/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/4 23:31:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 支持多语言的字符串特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ExtInfoI18NAttribute : ExtensionAttribute, IInformationAttribute
    {
        private const string DEFAULT_KEY = "ALL_REGIONS";
        private const string KV_PATTERN = @"(?<key>[\w|\-]+):(?<value>.+)";
        private readonly Regex regex = new Regex(KV_PATTERN);
        private Dictionary<string, string> pairsOfRegionAndValue;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="pairRegionAndValues"></param>
        public ExtInfoI18NAttribute(params string[] pairRegionAndValues)
        {
            pairsOfRegionAndValue = new Dictionary<string, string>();
            Load(pairRegionAndValues);
        }
        private void Load(string[] _creatingData)
        {
            if (pairsOfRegionAndValue.Count() != 0)
            {
                pairsOfRegionAndValue.Clear();
            }
            if (_creatingData == null)
            {
                pairsOfRegionAndValue.Add(DEFAULT_KEY, null);
                return;
            }
            foreach (string kv in _creatingData)
            {
                ParseAndAddToDict(kv);
            }
        }
        private void ParseAndAddToDict(string kv)
        {
            var match = regex.Match(kv);
            if (match.Success)
            {
                AddOrOverwrite(
                    match.Result("${key}").ToLower() , 
                    match.Result("${value}"));
            }
            else
            {
                AddOrOverwrite(DEFAULT_KEY, kv);
            }
        }
        private void AddOrOverwrite(string key, string value)
        {
            try
            {
                pairsOfRegionAndValue.Add(key, value);
            }
            catch (ArgumentException)
            {
                pairsOfRegionAndValue[key] = value;
            }
        }
        /// <summary>
        /// 键
        /// </summary>
        public virtual string Key => GetType().Name;
        /// <summary>
        /// 值
        /// </summary>
        public virtual object Value
        {
            get
            {
                return GetByCurrentLanCode();
            }
        }
        /// <summary>
        /// 尝试获取
        /// </summary>
        /// <returns></returns>
        private string GetByCurrentLanCode()
        {
            string crtLanCode = null;
            CallingBus.BaseApi.RunOnUIThread(() =>
            {
                crtLanCode = CallingBus.BaseApi.GetCurrentLanguageCode().ToLower();
            });

            if (pairsOfRegionAndValue.TryGetValue(crtLanCode, out string value))
            {
                return value;
            }
            else if (pairsOfRegionAndValue.TryGetValue(DEFAULT_KEY, out string defaultValue))
            {
                return defaultValue;
            }
            else
            {
                try
                {
                    return pairsOfRegionAndValue.First().Value;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
