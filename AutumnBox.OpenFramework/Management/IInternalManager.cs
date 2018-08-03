using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.ExtLibrary;
using System.Collections.Generic;
namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 总的拓展模块管理器
    /// </summary>
    public interface IInternalManager 
    {
        /// <summary>
        /// 管理的拓展模块路径
        /// </summary>
        string ExtensionPath { get; }
        /// <summary>
        /// 重载
        /// </summary>
        void Reload();
        /// <summary>
        /// 已加载的库管理器
        /// </summary>
        IEnumerable<ILibrarian> Librarians { get; }
        /// <summary>
        /// 获取所有的拓展模块包装器
        /// </summary>
        IEnumerable<IExtensionWarpper> Warppers { get; }
    }
}
