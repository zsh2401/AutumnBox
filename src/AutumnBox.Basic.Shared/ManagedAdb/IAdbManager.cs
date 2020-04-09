using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// Adb管理器
    /// </summary>
    public interface IAdbManager
    {
        /// <summary>
        /// ADB服务
        /// </summary>
        IAdbServer Server { get; }
        /// <summary>
        /// ADB可执行文件信息
        /// </summary>
        FileInfo AdbFile { get; }
        /// <summary>
        /// Fastboot可执行文件信息
        /// </summary>
        FileInfo FastbootFile { get; }
        /// <summary>
        /// Adb工具文件夹信息
        /// </summary>
        DirectoryInfo ToolsDir { get; }
    }
}
