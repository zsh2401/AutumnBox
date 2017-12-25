using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Connection
{
    public sealed class DeviceConnection
    {
        public Serial Serial { get { return DevInfo.Serial; } }
        public DeviceBasicInfo DevInfo { get; private set; }
        public void Reset(DeviceBasicInfo basicInfo)
        {
            this.DevInfo = basicInfo;
        }
        public void Reset()
        {
            this.DevInfo = new DeviceBasicInfo() { Status = DeviceStatus.None };
        }
        public AndroidShell GetShell()
        {
            return new AndroidShell(Serial);
        }
    }
}
