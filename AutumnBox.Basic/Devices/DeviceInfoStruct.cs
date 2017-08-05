using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public struct DeviceInfo {
        public string model;//型号
        public string brand;//厂商
        public string code;//代号
        public string id;//id
        public string androidVersion;//安卓版本
        public DeviceStatus deviceStatus;
    }
}
