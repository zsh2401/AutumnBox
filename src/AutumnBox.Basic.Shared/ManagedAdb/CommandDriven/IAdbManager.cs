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
        /// <exception cref="ObjectDisposedException">已经被释放</exception>
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
        /// 表示运行中的ADB SERVER地址
        /// </summary>
        /// <exception cref="ObjectDisposedException">对象被释放</exception>
        /// <exception cref="InvalidOperationException">服务状态异常</exception>
        IPEndPoint ServerEndPoint { get; }

        /// <summary>
        /// 进行初始化
        /// </summary>

        void Initialize();
    }
}
