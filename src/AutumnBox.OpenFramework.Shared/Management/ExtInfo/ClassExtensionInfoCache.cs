#nullable enable
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 模块信息缓存管理器
    /// </summary>
    public static class ClassExtensionInfoCache
    {
        /// <summary>
        /// 被缓存的
        /// </summary>
        private static readonly IDictionary<Type, ClassExtensionInfo> cached = new ConcurrentDictionary<Type, ClassExtensionInfo>();

        /// <summary>
        /// 从缓存中获取一个拓展模块信息
        /// </summary>
        /// <param name="classExtensionType"></param>
        /// <returns></returns>
        public static ClassExtensionInfo Acquire(Type classExtensionType)
        {
            if (cached.TryGetValue(classExtensionType, out ClassExtensionInfo result))
            {
                return result;
            }
            else
            {
                var instance = new ClassExtensionInfo(classExtensionType);
                cached.Add(classExtensionType, instance);
                return instance;
            }
        }

        /// <summary>
        /// 从缓存中获取一个拓展模块信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ClassExtensionInfo Acquire<T>() where T : IClassExtension
        {
            return Acquire(typeof(T));
        }
    }
}
