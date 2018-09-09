/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 16:56:38 (UTC +8:00)
** desc： ...
*************************************************/
using System.Collections.Generic;
using System.Linq;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.MultipleDevices
{
    public class DevicesGetter : IDevicesGetter
    {
        private readonly AdbCommand adbDevices = new AdbCommand("devices -l");
        private readonly FastbootCommand fastbootDevices = new FastbootCommand("devices");
        public IEnumerable<IDevice> GetDevices()
        {
            List<IDevice> result = new List<IDevice>();
            Adb(result);
            Fastboot(result);
            return result;
        }
        private void Adb(List<IDevice> devices)
        {
            var lineOutput = adbDevices.Execute().Output.LineOut;
            for (int i = 1; i < lineOutput.Count(); i++)
            {
                if (DeviceObjectFacotry.AdbTryParse(lineOutput[i], out IDevice device))
                {
                    devices.Add(device);
                }
            }
        }
        private void Fastboot(List<IDevice> devices)
        {
            var lineOutput = fastbootDevices.Execute().Output.LineOut;
            for (int i = 0; i < lineOutput.Count(); i++)
            {
                if (DeviceObjectFacotry.FastbootTryParse(lineOutput[i], out IDevice device))
                {
                    devices.Add(device);
                }
            }
        }
    }
}
