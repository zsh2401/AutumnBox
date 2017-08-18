using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.DebugTools;
using AutumnBox.Basic.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public event DevicesChangeHandler DevicesChange;
        private Task devicesListenerTask;
        private const string TAG = "DevicesListener";
        private const int defaultInterval = 1000;
        private readonly int interval;
        [Obsolete]
        internal DevicesListener(AdbTools at, FastbootTools ft, int interval = defaultInterval)
        {
            this.interval = interval;
        }
        internal DevicesListener() { }
        public void Stop()
        {
            //devicesListenerTask
            //Task.
        }
        public void Pause(int second = 1000)
        {
            devicesListenerTask.Wait(second);
        }
        public void Start()
        {
            if (DevicesChange == null)
            {
                throw new EventNotBoundException();
            }
            devicesListenerTask = new Task(this.Listener);
            devicesListenerTask.Start();
        }
        private void Listener()
        {
            DevicesHashtable last = new DevicesHashtable();
            new Adb().Execute("devices");
            while (true)
            {
                while (true)
                {
                    //此处重载了运算符,当执行+时,会把两个hashmap的值相加并返回
                    DevicesHashtable now = DevicesTools.GetDevices();
                    if (last != now)
                    {
                        last = now;
                        Log.d(TAG, "Devices Change");
                        DevicesChange?.Invoke(this, now);
                    }
                    Thread.Sleep(interval);
                }
            }
        }


    }
}
