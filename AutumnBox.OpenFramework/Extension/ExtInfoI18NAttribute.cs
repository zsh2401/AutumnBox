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
        private Dictionary<string, string> kvs;
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="pairRegionAndValues"></param>
        /// <param name="value"></param>
        public ExtInfoI18NAttribute(params string[] pairRegionAndValues)
        {
            Load(pairRegionAndValues);
        }
        private bool TryParse(string kv, ref string k, ref string v)
        {
            try {
                var splits = kv.Split(':');
                k = splits.First();
                v = string.Join("", splits, splits.Count() - 1);
                return true;
            } catch(Exception) {
                return false;
            }
        }
        private void Load(params string[] _kvs)
        {
            kvs = new Dictionary<string, string>();
            string currentKey = null;
            string currentValue = null;
            foreach (string kv in _kvs)
            {
                if (TryParse(kv, ref currentKey, ref currentValue))
                {
                    kvs.Add(currentKey, currentValue);
                }
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
            CallingBus.AutumnBox_GUI.RunOnUIThread(() =>
            {
                crtLanCode = CallingBus.AutumnBox_GUI.GetCurrentLanguageCode();
            });
            if (kvs.TryGetValue(crtLanCode, out string value))
            {
                return value;
            }
            try
            {
                return kvs.First().Value;
            }
            catch
            {
                return null;
            }
        }
    }
}
