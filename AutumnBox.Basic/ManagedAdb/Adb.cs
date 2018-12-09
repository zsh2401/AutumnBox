/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/9 18:02:46 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// AutumnBox.Basic中与ADB程序直接接触的管理层
    /// </summary>
    public static class Adb
    {
        /// <summary>
        /// ADB临时文件路径
        /// </summary>
        public static string AdbTmpPathOnDevice { get; } = "/data/local/tmp";
        /// <summary>
        /// 服务器
        /// </summary>
        public static IAdbServer Server { get; private set; }

        /// <summary>
        /// ADB文件
        /// </summary>
        public static FileInfo AdbFile { get; private set; }
        /// <summary>
        /// ADB文件路径
        /// </summary>
        public static string AdbFilePath => AdbFile.FullName;

        /// <summary>
        /// Fastboot文件
        /// </summary>
        public static FileInfo FastbootFile { get; private set; }
        /// <summary>
        /// Fastboot文件路径
        /// </summary>
        public static string FastbootFilePath => FastbootFile.FullName;

        /// <summary>
        /// ADB文件夹路径
        /// </summary>
        public static DirectoryInfo AdbToolsDir { get; private set; }

        private const string AUTUMNBOX_BASIC_ADB_TOOLS = "ManagedAdb/win32adb/";
        private const string AUTUMNBOX_BASIC_ADB_FILE = "ManagedAdb/win32adb/adb.exe";
        private const string AUTUMNBOX_BASIC_FASTBOOT_FILE = "ManagedAdb/win32adb/fastboot.exe";
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="adbToolsDir"></param>
        /// <param name="adbClient"></param>
        /// <param name="fastbootClient"></param>
        /// <param name="server"></param>
        /// <param name="startTheServer"></param>
        public static void Load(DirectoryInfo adbToolsDir,FileInfo adbClient, FileInfo fastbootClient, IAdbServer server, bool startTheServer)
        {
            //throw new AdbCommandFailedException(null,0);
            AdbToolsDir = adbToolsDir ?? throw new ArgumentNullException(nameof(adbToolsDir));
            AdbFile = adbClient ?? throw new ArgumentNullException(nameof(adbClient));
            FastbootFile = fastbootClient ?? throw new ArgumentNullException(nameof(fastbootClient));
            Server = server ?? throw new ArgumentNullException(nameof(server));
            if (startTheServer)
            {
                Server.Start();
            }
        }
    }
}
