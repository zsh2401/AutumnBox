#nullable enable
using AutumnBox.Leafx.ObjectManagement;
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
        /// 从缓存中获取一个拓展模块信息
        /// </summary>
        /// <param name="classExtensionType"></param>
        /// <returns></returns>
        public static ClassExtensionInfo Acquire(Type classExtensionType)
        {
            return ObjectCache<Type, ClassExtensionInfo>.Acquire(classExtensionType, () =>
            {
                return new ClassExtensionInfo(classExtensionType);
            });
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
