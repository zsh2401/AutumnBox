/*
 @zsh2401
 2017/9/6
 设备监听器,监听设备拔插
 */
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Util;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    /// <summary>
    /// 设备监听器
    /// </summary>
    public class DevicesListener
    {
        public delegate void DevicesChangeHandler(object obj, DevicesHashtable hs);
        public event DevicesChangeHandler DevicesChange;//当连接设备的情况变化时发生
        private Task devicesListenerTask;
        private const string TAG = "DevicesListener";
        private const int defaultInterval = 1000;
        private bool Continue = true;
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
            if (DevicesChange == null)
            {
                throw new EventNotBoundException();
            }
            devicesListenerTask = new Task(this.Listen);
            devicesListenerTask.Start();
        }
        private void Listen()
        {
            DevicesHashtable last = new DevicesHashtable();
            new Adb().Execute("devices");
            while (Continue == true)
            {
                //此处重载了运算符,当执行+时,会把两个hashmap的值相加并返回
                DevicesHashtable now = DevicesTools.GetDevices();
                if (last != now)
                {
                    last = now;
                    //GC.Collect();
                    Logger.D(TAG, "Devices Change");
                    DevicesChange?.Invoke(this, now);
                }
                Thread.Sleep(interval);
            }
        }
    }
}
