/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 23:51:51
** filename: DeviceHaveNoRootException.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备没有Root的异常
    /// </summary>
    public class DeviceHaveNoRootException : Exception
    {
        public DeviceSerial Device { get; private set; }
        public DeviceHaveNoRootException() { }
        public DeviceHaveNoRootException(DeviceSerial dev)
        {
            this.Device = dev;
        }
    }
}
