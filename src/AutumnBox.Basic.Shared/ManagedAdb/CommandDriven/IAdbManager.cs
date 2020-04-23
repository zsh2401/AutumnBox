using AutumnBox.Basic.Data;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// Adb管理器
    /// </summary>
    public interface IAdbManager : IDisposable, INotifyDisposed
    {
        /// <summary>
        /// 获取管理器
        /// </summary>
        ICommandProcedureManager OpenCommandProcedureManager();

        /// <summary>
        /// ADB客户端文件夹
        /// </summary>
        DirectoryInfo AdbClientDirectory { get; }

        /// <summary>
        /// ADB可执行文件
        /// </summary>
        FileInfo AdbExecutableFile { get; }

        /// <summary>
        /// fastboot可执行文件
        /// </summary>
        FileInfo FastbootExecutableFile { get; }

        /// <summary>
        /// 获取ADB服务地址
        /// </summary>
        IPEndPoint ServerEndPoint { get; }
    }
}
