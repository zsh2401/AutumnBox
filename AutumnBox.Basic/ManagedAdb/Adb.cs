/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/9 18:02:46 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.ManagedAdb
{
    /// <summary>
    /// Adb客户端
    /// </summary>
    public static class Adb
    {
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
        /// 默认加载
        /// </summary>
        public static void DefaultLoad()
        {
            Load(
                new DirectoryInfo(AUTUMNBOX_BASIC_ADB_TOOLS),
                new FileInfo(AUTUMNBOX_BASIC_ADB_FILE),
                new FileInfo(AUTUMNBOX_BASIC_FASTBOOT_FILE),
                new LocalAdbServer(),
                true
                );
        }
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="adbToolsDir"></param>
        /// <param name="adb"></param>
        /// <param name="fastboot"></param>
        /// <param name="server"></param>
        /// <param name="startTheServer"></param>
        public static void Load(DirectoryInfo adbToolsDir,FileInfo adb, FileInfo fastboot, IAdbServer server, bool startTheServer)
        {
            AdbToolsDir = adbToolsDir;
            AdbFile = adb;
            FastbootFile = fastboot;
            Server = server;
            if (startTheServer)
            {
                Server.Start();
            }
        }
    }
}
