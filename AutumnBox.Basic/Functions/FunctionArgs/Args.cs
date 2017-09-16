/*
 @zsh2401
 2017/9/6
 一些功能模块所需的参数
 */
using AutumnBox.Basic.Devices;
using System;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 为了标记他们都是功能模块的参数而设立的接口
    /// </summary>
    public interface IArgs { }

    /// <summary>
    /// 用于文件发送器和自定义REC刷入器
    /// </summary>
    public struct FileArgs:IArgs {
        public string[] files;
    }
    /// <summary>
    /// 用于重启器的参数
    /// </summary>
    public struct RebootArgs:IArgs {
        public RebootOptions rebootOption;
        public DeviceStatus nowStatus; 
    }
    /// <summary>
    /// 用于活动启动器的参数
    /// </summary>
    public struct ActivityLaunchArgs:IArgs {
        public string PackageName;
        public string ActivityName;
    }
}
