/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:09:32 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling.BuilderSchema
{
    public interface IDeviceSettableBuilder<TDeviceSettedReturn> : IArgBuilder
    {
        TDeviceSettedReturn Device(string serialNumber);
        TDeviceSettedReturn Device(IDevice device);
    }
}
