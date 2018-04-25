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
using AutumnBox.Basic.Device;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Threading;
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
        public IEnumerable<DeviceBasicInfo> DevicesList { get; }
        internal DevicesChangedEventArgs(IEnumerable<DeviceBasicInfo> devList)
        {
            DevicesList = devList;
        }
    }
    /// <summary>
    /// 设备拔插监视器
    /// </summary>
    public static class DevicesMonitor
    {
        private static DevicesMonitorCore core = new DevicesMonitorCore();
        /// <summary>
        /// 获取当前已连接设备列表
        /// </summary>
        public static DeviceBasicInfo[] CurrentDevices => core.CurrentDevices;
        /// <summary>
        /// 设备拔插时发生
        /// </summary>
        public static event DevicesChangedHandler DevicesChanged
        {
            add { core.DevicesChanged += value; }
            remove { core.DevicesChanged -= value; }
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
            private readonly int _interval;
            private Thread coreThread;
            private IDevicesGetter _devGetter;
            public event DevicesChangedHandler DevicesChanged
            {
                add
                {
                    DevicesChangedInner += value;
                }
                remove
                {
                    DevicesChangedInner -= value;
                }
            }
            private event DevicesChangedHandler DevicesChangedInner;
            private DevicesList crtDevices;
            public DeviceBasicInfo[] CurrentDevices
            {
                get
                {
                    return crtDevices.ToArray();
                }
            }
            public DevicesMonitorCore(int interval = defaultInterval)
            {
                this._interval = interval;
                _devGetter = new DevicesGetter();
                crtDevices = new DevicesList();
            }
            public void Begin()
            {
                if (coreThread?.IsAlive == true) return;
                coreThread = new Thread(Listen);
                coreThread.Start();
                coreThread.IsBackground = true;
            }
            private void Listen()
            {
                DevicesList _new;
                while (true)
                {
                    _new = _devGetter.GetDevices();
                    _new.ForEach((d)=> { Logger.Debug(this,d); });
                    if (_new != crtDevices)
                    {
                        //Logger.Info(this, "Devices Changed");
                        crtDevices = _new;
                        this.DevicesChangedInner?.Invoke(this, new DevicesChangedEventArgs(crtDevices));
                    }
                    Thread.Sleep(_interval);
                }
            }
            public void Cancel()
            {
                coreThread.Abort();
            }
        }
    }
}
