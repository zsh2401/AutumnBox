using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.DebugTools;
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
        private Thread devicesListenerThread;
        private FastbootTools ft;
        private AdbTools at;
        private string TAG = "DevicesListener";
        private int interval;
        internal DevicesListener(AdbTools at, FastbootTools ft,int interval = 500) {
            this.interval = interval;
            this.at = at;
            this.ft = ft;
        }
        public void Stop() {
            devicesListenerThread.Abort();
        }
        public void Start() {
            devicesListenerThread = new Thread(this.Listener);
            devicesListenerThread.Name = "DevicesListener";
            devicesListenerThread.Start();
        }
        private void Listener() {
            DevicesHashtable last= new DevicesHashtable();
            at.Execute("devices");
            while (true) {
                while (true)
                {
                    //此处重载了运算符,当执行+时,会把两个hashmap的值相加并返回
                    DevicesHashtable now = at.GetDevices() + ft.GetDevices();
                    if (last != now)
                    {
                        last = now;
                        Log.d(TAG,"Devices Change");
                        DevicesChange?.Invoke(this, now);
                    }
                    Thread.Sleep(interval);
                }
            }
        }
    }
}
