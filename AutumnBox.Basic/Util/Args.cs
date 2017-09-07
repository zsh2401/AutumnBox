/*
 @zsh2401
 2017/9/6
 一些功能模块所需的参数
 */
using AutumnBox.Basic.Devices;
namespace AutumnBox.Basic.Util
{
    public interface IArgs { }
    public struct FileArgs:IArgs {
        public string[] files;
    }
    public struct RebootArgs:IArgs {
        public RebootOptions rebootOption;
        public DeviceStatus nowStatus; 
    }
    public struct ApplicationLaunchArgs {
        public string PackageName;
        public string ActivityName;
    }
}
