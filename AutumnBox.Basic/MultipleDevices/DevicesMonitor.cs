/* =============================================================================*\
*
* Filename: DevicesMonitor.cs
* Description: 
*
* Version: 1.0
* Created: 8/18/2017 22:09:36(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Support.Log;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace AutumnBox.Basic.MultipleDevices
{
    /// <summary>
    /// 当连接设备拔插时的触发的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DevicesChangedHandler(object sender, DevicesChangedEventArgs e);
    /// <summary>
    /// 连接设备变化的事件的参数
    /// </summary>
    public class DevicesChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 已连接设备列表
        /// </summary>
        public DevicesList DevicesList { get; }
        internal DevicesChangedEventArgs(DevicesList devList)
        {
            DevicesList = devList;
        }
    }
    /// <summary>
    /// 设备拔插监视器
    /// </summary>
    public static class DevicesMonitor
    {
        /// <summary>
        /// 设备拔插时发生
        /// </summary>
        public static event DevicesChangedHandler DevicesChanged;

        private static DevicesMonitorCore core;
        static DevicesMonitor()
        {
            core = new DevicesMonitorCore();
        }
        /// <summary>
        /// 开始监视
        /// </summary>
        public static void Begin()
        {
            core.Begin();
        }

        /// <summary>
        /// 停止/暂停监视
        /// </summary>
        public static void Stop()
        {
            core.Cancel();
        }
        /// <summary>
        /// 完全静态类很不OOP,所以....
        /// </summary>
        private sealed class DevicesMonitorCore
        {
            private const int defaultInterval = 2000;
            private bool _continue = true;
            private readonly int _interval;
            private IDevicesGetter _devGetter = new DevicesGetter();
            public DevicesMonitorCore(int interval = defaultInterval)
            {
                this._interval = interval;
            }
            public void Begin()
            {
                _continue = true;
                ListenAsync();
            }
            private async void ListenAsync()
            {
                var last = new DevicesList();
                while (_continue)
                {
                    var now = await Task.Run(() =>
                    {
                        Thread.Sleep(_interval);
                        return _devGetter.GetDevices();
                    });
                    if (now != last)
                    {
                        Logger.Warn("DevicesGetter", "Devices Changed");
                        last = now;
                        DevicesChanged?.Invoke(this, new DevicesChangedEventArgs(now));
                    }
                }
            }
            public void Cancel()
            {
                _continue = false;
            }
        }
    }
}
