#nullable enable
#nullable enable
using System;
using System.Collections.Generic;

namespace AutumnBox.Leafx.Enhancement.ClassTextKit
{
    public partial class ClassTextReader
    {
        /// <summary>
        /// 快速地读取
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键</param>
        /// <exception cref="ArgumentNullException">键为空</exception>
        /// <exception cref="KeyNotFoundException">找不到键</exception>
        /// <exception cref="InvalidOperationException">获取值时,特性内部异常</exception>
        /// <returns></returns>
        public static string Read<T>(string key)
        {
            return GetReader<T>()[key];
        }
    }
}
