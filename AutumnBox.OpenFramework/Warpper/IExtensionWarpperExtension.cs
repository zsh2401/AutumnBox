/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 7:04:12 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Warpper
{
    public static class IExtensionWarpperExtension
    {
        public static bool HasState(this IExtensionWarpper warpper, DeviceState state)
        {
            return warpper.Info.RequiredDeviceStates.HasFlag(state);
        }
    }
}
