/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:53:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using System;
using System.Net;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 网络连接设备
    /// </summary>
    public sealed class NetDevice : DeviceBase
    {
        /// <summary>
        /// IP
        /// </summary>
        public IPEndPoint IPEndPoint { get; internal set; }
        /// <summary>
        /// 断开连接
        /// </summary>
        /// <param name="closeNetDebugging"></param>
        public void Disconnect(bool closeNetDebugging = true)
        {
            if (closeNetDebugging)
            {
                new AdbCommand(this, $"usb").Execute()
                .ThrowIfExitCodeNotEqualsZero();
            }
            else
            {
                new AdbCommand($"disconnect {SerialNumber}").Execute()
                .ThrowIfExitCodeNotEqualsZero();
            }
        }
    }
}
