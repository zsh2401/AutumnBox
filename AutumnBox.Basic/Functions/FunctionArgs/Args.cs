/*
 @zsh2401
 2017/9/6
 一些功能模块所需的参数
 */
using AutumnBox.Basic.Devices;
using System;

namespace AutumnBox.Basic.Functions
{
    public interface IArgs { }
    public struct FileArgs:IArgs {
        public string[] files;
    }
    [Obsolete]
    public struct RebootArgs:IArgs {
        public RebootOptions rebootOption;
        public DeviceStatus nowStatus; 
    }
    public struct ActivityLaunchArgs:IArgs {
        public string PackageName;
        public string ActivityName;
    }
}
