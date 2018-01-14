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
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.Function.Args
{

    /// <summary>
    /// 用于文件发送器和自定义REC刷入器
    /// </summary>
    public class FileArgs : ModuleArgs
    {
        public FileArgs(DeviceBasicInfo device) : base(device) { }
        public string[] files;
    }
    /// <summary>
    /// 用于活动启动器的参数
    /// </summary>
    public class ActivityLaunchArgs : ModuleArgs
    {
        public ActivityLaunchArgs(DeviceBasicInfo device) : base(device) { }
        public string PackageName;
        public string ActivityName;
    }
    public class InstallApkArgs : ModuleArgs
    {
        public InstallApkArgs(DeviceBasicInfo device) : base(device) { }
        public string ApkPath { get; set; }
    }
}
