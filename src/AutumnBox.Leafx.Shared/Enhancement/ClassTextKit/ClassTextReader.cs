#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// 类文本读取器
    /// </summary>
    public partial class ClassTextReader
    {
        /// <summary>
        /// 构造一个ClassText加载器
        /// </summary>
        /// <param name="type"></param>
        public ClassTextReader(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            Type = type;
            var dc = new Dictionary<string, ClassTextAttribute>();
            foreach (var attr in type.GetCustomAttributes<ClassTextAttribute>(true))
            {
                dc.Add(attr.Key.ToLower(), attr);
            }
            attributes = new ReadOnlyDictionary<string, ClassTextAttribute>(dc);
        }

        /// <summary>
        /// 装载的类型
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 内部维护特性字典
        /// </summary>
        private readonly ReadOnlyDictionary<string, ClassTextAttribute> attributes;

        /// <summary>
        /// 通过索引器的方式获取类文本值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="regionCode">区域码,不填默认为当前线程区域码 ,如: zh-CN</param>
        /// <exception cref="KeyNotFoundException">进行了检索,但没有找到对应的键</exception>
        /// <returns>应获得类文本值</returns>
        public string this[string key, string? regionCode = null]
        {
            get
            {
                return Get(key, regionCode);
            }
        }

        /// <summary>
        /// 根据指定的区域码获取语言
        /// </summary>
        /// <param name="key"></param>
        /// <param name="_regionCode"></param>
        /// <exception cref="KeyNotFoundException">进行了检索,但没有找到对应的键</exception>
        /// <returns></returns>
        public string Get(string key, string? _regionCode = null)
        {
            return attributes[key].GetText(_regionCode);
        }

        /// <summary>
        /// 尝试获取,不会抛出异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="_regionCode"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out string? value, string? _regionCode = null)
        {
            try
            {
                value = Get(key, _regionCode);
                return true;
            }
            catch
            {
                value = default;
                return false;
            }
        }
    }
}
