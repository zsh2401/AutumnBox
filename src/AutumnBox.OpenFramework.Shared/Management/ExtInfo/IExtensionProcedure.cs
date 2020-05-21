#nullable enable
#nullable enable
using AutumnBox.Leafx.Container;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{

    /// <summary>
    /// 拓展模块事务
    /// </summary>
    public interface IExtensionProcedure : IDisposable
    {
        /// <summary>
        /// 注入源
        /// </summary>
        ILake? Source { get; set; }

        /// <summary>
        /// 额外参数
        /// </summary>
        Dictionary<string, object>? Args { get; set; }

        /// <summary>
        /// 运行
        /// </summary>
        /// <returns></returns>
        object? Run();
    }
}
