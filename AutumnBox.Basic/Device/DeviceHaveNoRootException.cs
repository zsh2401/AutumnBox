/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 23:51:51
** filename: DeviceHaveNoRootException.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备没有Root的异常
    /// </summary>
    [Serializable]
    public class DeviceHaveNoRootException : Exception
    {
        /// <summary>
        /// 具体的设备
        /// </summary>
        public DeviceSerialNumber Device { get; private set; }
        /// <summary>
        /// 创建DeviceHaveNoRootException的新实例
        /// </summary>
        public DeviceHaveNoRootException() {
        }
        /// <summary>
        /// 创建DeviceHaveNoRootException的新实例并说明设备
        /// </summary>
        public DeviceHaveNoRootException(DeviceSerialNumber dev)
        {
            this.Device = dev;
        }
    }
}
