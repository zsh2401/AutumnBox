#nullable enable
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// 类文本读取器
    /// </summary>
    public sealed partial class ClassTextReader
    {
        /// <summary>
        /// 构造一个ClassText加载器
        /// </summary>
        /// <param name="type"></param>
        private ClassTextReader(Type type)
        {
            if (type is null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            Type = type;
            var dc = new Dictionary<string, ClassTextAttribute>();
            foreach (var attr in type.GetCustomAttributes<ClassTextAttribute>(true))
            {
                dc.Add(attr.Key, attr);
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
        /// 文本索引器
        /// </summary>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException">键为空</exception>
        /// <exception cref="KeyNotFoundException">找不到键</exception>
        /// <exception cref="InvalidOperationException">获取值时,特性内部异常</exception>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }
                try
                {
                    return attributes[key].Value;
                }
                catch (KeyNotFoundException e)
                {
                    throw e;
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException("Can not read key", e);
                }
            }
        }

        /// <summary>
        /// 尝试获取,不会抛出异常
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(string key, out string? value)
        {
            try
            {
                value = this[key];
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
