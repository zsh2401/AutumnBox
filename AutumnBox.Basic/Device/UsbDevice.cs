/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:53:10 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
        /// <param name="tryConnect">是否在开启后尝试连接</param>
        public void OpenNetDebugging(ushort port, bool tryConnect = false)
        {
            IPAddress ip = null;
            if (tryConnect)
            {
                ip = this.GetLanIP();
            }
            this.Adb($"tcpip {port}").ThrowIfExitCodeNotEqualsZero();
            Task.Run(() =>
            {

                if (ip != null)
                {
                    Thread.Sleep(2000);
                    new AdbCommand($"connect {ip}:{port}").Execute().ThrowIfExitCodeNotEqualsZero();
                }
            });
        }
    }
}
