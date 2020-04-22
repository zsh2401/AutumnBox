/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 16:56:38 (UTC +8:00)
** desc： ...
*************************************************/
using System.Collections.Generic;
using System.Linq;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;

namespace AutumnBox.Basic.MultipleDevices
{
    /// <summary>
    /// 已连接设备获取器
    /// </summary>
    public class DevicesGetter : IDevicesGetter
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IDevice> GetDevices()
        {
            List<IDevice> result = new List<IDevice>();
            Adb(result);
            Fastboot(result);
            return result;
        }

        /// <summary>
        /// 获取ADB设备
        /// </summary>
        /// <param name="devices"></param>
        private void Adb(List<IDevice> devices)
        {
            using var cmd = BasicBooter.CommandProcedureManager.OpenADBCommand(null, "devices");
            var lineOutput = cmd.Execute().Output.LineOut;
            for (int i = 1; i < lineOutput.Count(); i++)
            {
                if (DeviceBase.TryParse(lineOutput[i], out IDevice device))
                {
                    devices.Add(device);
                }
            }
        }

        /// <summary>
        /// 获取fastboot设备
        /// </summary>
        /// <param name="devices"></param>
        private void Fastboot(List<IDevice> devices)
        {
            using var cmd = BasicBooter.CommandProcedureManager.OpenADBCommand(null, "devices");
            var lineOutput = cmd.Execute().Output.LineOut;
            for (int i = 0; i < lineOutput.Count(); i++)
            {
                if (DeviceBase.TryParse(lineOutput[i], out IDevice device))
                {
                    devices.Add(device);
                }
            }
        }
    }
}
