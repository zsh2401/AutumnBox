using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public interface IDevicesGetter
    {
        void GetDevices(out DevicesList devList);
        DevicesList GetDevices();
    }
}
