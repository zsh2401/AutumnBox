/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:34:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.DPCommand;
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.Basic.Device
{
    public static class DeviceExtension
    {
        public static ICommand GetShellCommand(this IDevice device, string sh)
        {
            throw new NotImplementedException();
        }
        public static IBroadcastSender GetBroadcastSender(this IDevice device)
        {
            throw new NotImplementedException();
        }
        public static IBuildPropGetter GetBuildPropGetter(this IDevice device)
        {
            device.ThrowIfNotAlive();
            throw new NotImplementedException();
        }
        public static IActivityManager GetActivityManager(this IDevice device)
        {
            throw new NotImplementedException();
        }
        public static IPackageManager GetPackageManager(this IDevice device)
        {
            throw new NotImplementedException();
        }
        public static IServiceManager GetServiceManager(this IDevice device)
        {
            throw new NotImplementedException();
        }

    }
}
