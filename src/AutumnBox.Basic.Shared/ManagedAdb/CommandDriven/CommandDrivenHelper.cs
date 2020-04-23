using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// CommandDriven命名空间下的一些常量
    /// </summary>
    public static class CommandDrivenHelper
    {
        /// <summary>
        /// Path环境变量的键
        /// </summary>
        public const string ENV_KEY_PATH = "PATH";

        /// <summary>
        /// 安卓ADB服务器端口键
        /// </summary>
        public const string ENV_KEY_ANDROID_ADB_SERVER_PORT = "ANDROID_ADB_SERVER_PORT";

        /// <summary>
        /// 初始化ADB环境
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="dir"></param>
        /// <param name="port"></param>
        public static void InitializeAdbEnvironment(this CommandProcedure cmd, DirectoryInfo dir, ushort port)
        {
            cmd.ExtraPathVariables.Add(dir.FullName);
            cmd.ExtraEnvironmentVariables[ENV_KEY_ANDROID_ADB_SERVER_PORT] = port.ToString();
        }
    }
}
