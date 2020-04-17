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
        /// <param name="args">返回任意的结果,通常为NULL</param>
        /// <returns></returns>
        object Main(Dictionary<string, object> args);
    }
}
