#nullable enable
/*

* ==============================================================================
*
* Filename: ObjectCache
* Description: 
*
* Version: 1.0
* Created: 2020/5/5 14:27:56
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.ObjectManagement
{
    /// <summary>
    /// 使用静态泛型技巧,为每一种键值对建立一个公共的缓存器
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public static class ObjectCache<TKey, TValue>
    {
        /// <summary>
        /// 内部维护的缓存
        /// </summary>
        private readonly static IDictionary<TKey, TValue> cache = new ConcurrentDictionary<TKey, TValue>();

        /// <summary>
        /// 清空缓存
        /// </summary>
        public static void ClearCache()
        {
            cache.Clear();
        }

        /// <summary>
        /// 已缓存数量
        /// </summary>
        public static int CachedCount => cache.Count;

        /// <summary>
        /// 从缓存中请求一个对象
        /// </summary>
        /// <param name="key">唯一标识对象的键</param>
        /// <param name="valueFactory">当对象不存在时,用于新建对象并缓存的工厂函数</param>
        /// <exception cref="InvalidOperationException">当对象不存在,并且工厂函数为null</exception>
        /// <returns>被检索到的对象</returns>
        public static TValue Acquire(TKey key, Func<TValue>? valueFactory = null)
        {
            if (cache.TryGetValue(key, out TValue value))
            {
                return value;
            }
            else if (valueFactory != null)
            {
                var tmp = valueFactory();
                cache.Add(key, tmp);
                return tmp;
            }
            else
            {
                throw new InvalidOperationException("Can not acquire value from cache and default factory is null");
            }
        }
    }
}
