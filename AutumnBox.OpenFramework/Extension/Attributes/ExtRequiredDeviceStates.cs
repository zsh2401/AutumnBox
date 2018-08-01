/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 20:45:49 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Extension.Attributes
{
    public class ExtRequiredDeviceStatesAttribute : Attribute
    {
        public DeviceState Value { get; set; }
        public ExtRequiredDeviceStatesAttribute(DeviceState state)
        {
            this.Value = state;
        }
    }
}
