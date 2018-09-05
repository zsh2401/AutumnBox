/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:53:10 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 通过USB连接的设备
    /// </summary>
    public sealed class UsbDevice : DeviceBase
    {
        /// <summary>
        /// 开启网络调试
        /// </summary>
        /// <param name="port"></param>
        public void OpenNetDebugging(ushort port)
        {
            this.Adb($"tcpip {port}").ThrowIfExitCodeNotEqualsZero() ;
        }
    }
}
