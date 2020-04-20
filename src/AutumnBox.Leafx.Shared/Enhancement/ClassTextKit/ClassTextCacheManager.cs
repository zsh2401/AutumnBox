#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// ClassText缓存管理器
    /// </summary>
    public static class ClassTextCacheManager
    {
        /// <summary>
        /// 被缓存的读取器
        /// </summary>
        public static IDictionary<Type, ClassTextReader> CachedReaders { get; }
        static ClassTextCacheManager()
        {
            CachedReaders = new ConcurrentDictionary<Type, ClassTextReader>();
        }
    }
}
