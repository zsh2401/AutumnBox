#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// ClassText读取器
    /// </summary>
    partial class ClassTextReader
    {
        /// <summary>
        /// 泛型读取方法
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <returns></returns>
        public static ClassTextReader GetReader<TClass>()
        {
            return GetReader(typeof(TClass));
        }

        /// <summary>
        /// 通过实例读取
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static ClassTextReader GetReader(object instance)
        {
            if (instance is null)
            {
                throw new ArgumentNullException(nameof(instance));
            }

            return GetReader(instance.GetType());
        }

        /// <summary>
        /// 读取类型
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ClassTextReader GetReader(Type t)
        {
            var cache = ClassTextCacheManager.CachedReaders;
            if (cache.ContainsKey(t))
            {
                return cache[t];
            }
            else
            {
                var reader = new ClassTextReader(t);
                cache.Add(t, reader);
                return reader;
            }
        }
    }
}
