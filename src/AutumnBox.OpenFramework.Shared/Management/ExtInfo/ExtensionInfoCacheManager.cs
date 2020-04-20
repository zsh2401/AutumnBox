#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 模块信息缓存管理器
    /// </summary>
    public static class ExtensionInfoCacheManager
    {
        /// <summary>
        /// 被缓存的
        /// </summary>
        public static IDictionary<Type, IExtensionInfo> Cached { get; }

        /// <summary>
        /// 静态构建
        /// </summary>
        static ExtensionInfoCacheManager()
        {
            Cached = new ConcurrentDictionary<Type, IExtensionInfo>();
        }
    }
}
