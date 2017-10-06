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
namespace AutumnBox.Basic.Devices
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Util;
    using System;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;
    /// <summary>
    /// 设备监听器
    /// </summary>
    public sealed class DevicesMonitor:BaseObject,IDisposable
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
        public void Start()
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
                if (Process.GetProcessesByName("adb").Length == 0) CommandExecuter.Start();
                var now =  executer.GetDevices();
                if (now != last)
                {
                    LogD("Devices Change");
                    last = now;
                    DevicesChanged.Invoke(this, new DevicesChangeEventArgs(now));
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
