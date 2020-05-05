#nullable enable
using AutumnBox.Leafx.ObjectManagement;
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
        /// 参与缓存机制，申请一个ClassTextReader
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static ClassTextReader Acquire(Type t)
        {
            return ObjectCache<Type, ClassTextReader>.Acquire(t, () => new ClassTextReader(t));
        }

        /// <summary>
        /// 参与缓存机制，申请一个ClassTextReader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ClassTextReader Acquire<T>()
        {
            return Acquire(typeof(T));
        }
    }
}
