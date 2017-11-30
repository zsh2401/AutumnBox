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
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 连接设备变化的事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DevicesChangedHandler(object sender, DevicesChangedEventArgs e);
    /// <summary>
    /// 连接设备变化的事件的参数
    /// </summary>
    public class DevicesChangedEventArgs : EventArgs
    {
        public DevicesList DevicesList { get; }
        public DevicesChangedEventArgs(DevicesList devList)
        {
            DevicesList = devList;
        }
    }
    /// <summary>
    /// 设备监听器
    /// </summary>
    public sealed class DevicesMonitor : IDisposable
    {
        public event DevicesChangedHandler DevicesChanged;//当连接设备的情况变化时发生
        private const int defaultInterval = 1000;
        private Task devicesListenerTask;
        private bool Continue = true;
        private DevicesList last = new DevicesList();
        private readonly int interval;
        public DevicesMonitor(int interval = defaultInterval)
        {
            this.interval = interval;
        }
        /// <summary>
        /// 开始监听
        /// </summary>
        public  void Start()
        {
            if (DevicesChanged == null)
            {
                throw new EventNotBoundException("不绑定事件,那么开始这个监听器有毛线用处啊!!!");
            }
            Continue = true;
            devicesListenerTask = new Task(this.Listening);
            devicesListenerTask.Start();
        }
        /// <summary>
        /// 停止监听
        /// </summary>
        public void Stop()
        {
            Continue = false;
        }
        /// <summary>
        /// 暂停监听
        /// </summary>
        /// <param name="second">暂停时间(毫秒)</param>
        public void Pause(int second = 1000)
        {
            devicesListenerTask.Wait(second);
        }

        /// <summary>
        /// 无限循环的监听主函数
        /// </summary>
        private void Listening()
        {
            IDevicesGetter executer = new DevicesGetter();
            DevicesList last = new DevicesList();
            while (Continue)
            {
                var now = executer.GetDevices();
                if (now != last)
                {
                    Logger.D("Devices Change");
                    last = now;
                    Logger.D("DevicesChanged?.invoke()");
                    DevicesChanged?.Invoke(this, new DevicesChangedEventArgs(now));
                }
                Thread.Sleep(interval);
            }
        }
        private async void ListenAsync()
        {
            IDevicesGetter executer = new DevicesGetter();
            DevicesList last = new DevicesList();
            while (Continue)
            {
                var now = await Task.Run(() => { return executer.GetDevices(); });
                if (now != last)
                {
                    Logger.D("Devices Change");
                    last = now;
                    DevicesChanged.Invoke(this, new DevicesChangedEventArgs(now));
                }
                Thread.Sleep(interval);
            }
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            devicesListenerTask.Dispose();
        }
    }
}
