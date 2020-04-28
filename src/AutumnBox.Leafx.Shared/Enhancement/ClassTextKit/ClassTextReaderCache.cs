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
    public static class ClassTextReaderCache
    {
        /// <summary>
        /// 被缓存的读取器
        /// </summary>
        private readonly static IDictionary<Type, ClassTextReader> cached = new ConcurrentDictionary<Type, ClassTextReader>();

        public static ClassTextReader Acquire(Type t)
        {
            if (cached.TryGetValue(t, out ClassTextReader reader))
            {
                return reader;
            }
            else
            {
                var classTextReader = new ClassTextReader(t);
                cached.Add(t, classTextReader);
                return classTextReader;
            }
        }

        public static ClassTextReader Acquire<T>()
        {
            return Acquire(typeof(T));
        }
    }
}
