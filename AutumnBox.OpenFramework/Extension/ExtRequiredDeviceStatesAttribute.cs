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

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 拓展模块运行所需的设备状态
    /// 秋之盒将确保只有设备符合该状态时才可以调用该模块
    /// </summary>
    public class ExtRequiredDeviceStatesAttribute : ExtInfoAttribute
    {
        public ExtRequiredDeviceStatesAttribute(DeviceState state):base(state)
        {
        }
    }
}
