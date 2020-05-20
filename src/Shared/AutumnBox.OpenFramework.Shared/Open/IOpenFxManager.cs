/*

* ==============================================================================
*
* Filename: IOpenFxManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 16:07:58
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 开放框架管理器
    /// </summary>
    public interface IOpenFxManager
    {
        /// <summary>
        /// 获取所有拓展模块包装器
        /// </summary>
        IEnumerable<IExtensionInfo> Extensions { get; }

        /// <summary>
        /// 获取所有库管理器
        /// </summary>
        List<ILibrarian> Librarians { get; }

        /// <summary>
        /// 获取库管理器
        /// </summary>
        ILibrarian LibrarianOf(object context);

        /// <summary>
        /// 当拓展列表发生变动时触发
        /// </summary>
        event EventHandler ExtensionsChanged;

        /// <summary>
        /// 引起事件
        /// </summary>
        void RaiseExtensionsChangedEvent();

        /// <summary>
        /// 获取SDK等级
        /// </summary>
        int SDKLevel { get; }

        /// <summary>
        /// 获取框架的版本
        /// </summary>
        Version OpenFxVersion { get; }
    }
}
