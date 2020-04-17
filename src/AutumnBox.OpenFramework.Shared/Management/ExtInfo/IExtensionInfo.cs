#nullable enable
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Management.ExtInfo
{
    /// <summary>
    /// 拓展模块信息
    /// </summary>
    public interface IExtensionInfo
    {
        /// <summary>
        /// 拓展模块唯一标识
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 拓展模块元数据
        /// </summary>
        IReadOnlyDictionary<string, ValueReader> Metadata { get; }

        /// <summary>
        /// 获取拓展模块运行器
        /// </summary>
        /// <returns></returns>
        IExtensionProcedure Procedure { get; }
    }
}
