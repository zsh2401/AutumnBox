/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:30:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 初始化时的参数
    /// </summary>
    public class InitArgs { }
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
    public class StopArgs{}
    /// <summary>
    /// OnDestory()参数
    /// </summary>
    public class DestoryArgs { }
}
