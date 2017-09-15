/*
 @zsh2401
 2017/9/8
 设备监听器,监听设备拔插
 */
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 设备监听器
    /// </summary>
    public sealed class DevicesListener:BaseObject,IDisposable
    {
        public delegate void DevicesChangeHandler(object sender, DevicesList hs);
        public event DevicesChangeHandler DevicesChange;//当连接设备的情况变化时发生
        private Task devicesListenerTask;
        private const int defaultInterval = 2000;
        private bool Continue = true;
        private DevicesList last = new DevicesList();
        private readonly int interval;
        public DevicesListener(int interval = defaultInterval)
        {
            this.interval = interval;
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
        /// 开始监听
        /// </summary>
        public void Start()
        {
            Continue = true;
            if (DevicesChange == null)
            {
                throw new EventNotBoundException();
            }
            devicesListenerTask = new Task(this.Listen);
            devicesListenerTask.Start();
        }
        private void Listen()
        {
            while (Continue)
            {
                if (Process.GetProcessesByName("adb").Length == 0) new CommandExecuter().Execute("start-server");
                //此处重载了运算符,当执行+时,会把两个hashmap的值相加并返回
                DevicesList now = DevicesHelper.GetDevices();
                if (last != now)
                {
                    last = now;
                    LogD("Devices Change");
                    DevicesChange?.Invoke(this, now);
                }
                Thread.Sleep(interval);
            }
        }

        public void Dispose()
        {
            devicesListenerTask.Dispose();
        }
    }
}
