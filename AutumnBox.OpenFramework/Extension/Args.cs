/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:30:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 初始化时的参数
    /// </summary>
    public class InitArgs { }
    /// <summary>
    /// 运行检测参数
    /// </summary>
    public class RunCheckArgs
    {
        /// <summary>
        /// 设备信息
        /// </summary>
        public DeviceBasicInfo DeviceInfo { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="devInfo"></param>
        public RunCheckArgs(DeviceBasicInfo devInfo)
        {
            this.DeviceInfo = devInfo;
        }
    }
    /// <summary>
    /// OnStartCommand()参数
    /// </summary>
    public class StartArgs
    {
        /// <summary>
        /// 目标设备
        /// </summary>
        public DeviceBasicInfo Device { get; set; }
    }
    /// <summary>
    /// OnStopCommand()参数
    /// </summary>
    public class StopArgs { }
    /// <summary>
    /// OnDestory()参数
    /// </summary>
    public class DestoryArgs {
        public bool IsReload { get; internal set; } = false;
    }
    /// <summary>
    /// OnClean()参数
    /// </summary>
    public class CleanArgs { }
}
