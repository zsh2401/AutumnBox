#nullable enable
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    /// <summary>
    /// ClassText一些实用拓展方法
    /// </summary>
    public static class ClassTextExtension
    {
        /// <summary>
        /// 根据键,获取实例的类型的ClassText值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="InvalidOperationException">内部操作失败</exception>
        /// <exception cref="KeyNotFoundException">加载了读取器但未能找到对应值</exception>
        /// <returns>相应的值</returns>
        public static string GetClassText(this object obj, string key) => obj.GetType().GetClassText(key);

        /// <summary>
        /// 根据键,获取类型上的ClassText值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="key"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="InvalidOperationException">内部操作失败</exception>
        /// <exception cref="KeyNotFoundException">加载了读取器但未能找到对应值</exception>
        /// <returns>相应的值</returns>
        public static string GetClassText(this Type type, string key)
        {
            return ClassTextReaderCache.Acquire(type)[key];
        }
    }
}
