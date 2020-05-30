using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Collections.Generic;
namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 总的拓展模块管理器
    /// </summary>
    public interface ILibsManager
    {
        /// <summary>
        /// 重载
        /// </summary>
        void Reload();

        /// <summary>
        /// 已加载的库管理器
        /// </summary>
        IEnumerable<ILibrarian> Librarians { get; }

        /// <summary>
        /// 拓展模块注册表
        /// </summary>
        ICollection<IRegisteredExtensionInfo> ExtensionRegistry { get; }

        /// <summary>
        /// 当拓展模块注册表发生变化时触发
        /// </summary>
        event EventHandler ExtensionRegistryModified;
    }
}
