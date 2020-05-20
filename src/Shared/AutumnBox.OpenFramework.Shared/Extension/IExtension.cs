#nullable enable
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 只要实现此接口，均会被视为拓展模块
    /// </summary>
    public interface IExtension : IDisposable
    {
        /// <summary>
        /// 主方法
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns>方法返回值</returns>
        object? Main(Dictionary<string, object> args);
    }
}
