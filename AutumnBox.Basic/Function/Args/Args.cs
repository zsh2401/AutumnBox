/* =============================================================================*\
*
* Filename: Args.cs
* Description: 
*
* Version: 1.0
* Created: 9/8/2017 16:17:12(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;

namespace AutumnBox.Basic.Function.Args
{

    /// <summary>
    /// 用于文件发送器和自定义REC刷入器
    /// </summary>
    public class FileArgs : ModuleArgs
    {
        public string[] files;
    }
    /// <summary>
    /// 用于重启器的参数
    /// </summary>
    public class RebootArgs : ModuleArgs
    {
        public RebootOptions rebootOption;
        public DeviceStatus nowStatus;
    }
    /// <summary>
    /// 用于活动启动器的参数
    /// </summary>
    public class ActivityLaunchArgs : ModuleArgs
    {
        public string PackageName;
        public string ActivityName;
    }
    public class InstallApkArgs : ModuleArgs {
        public string ApkPath { get; set; }
    }
}
