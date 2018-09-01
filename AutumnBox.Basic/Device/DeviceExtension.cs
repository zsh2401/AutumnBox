/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:34:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.AppFx.Impl;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.Util;
using System;
using System.Windows.Input;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备对象拓展
    /// </summary>
    public static class DeviceExtension
    {
        public static Tuple<Output, int> Shell(this IDevice device,string sh)
        {
            throw new NotImplementedException();
        }
        public static Tuple<Output, int> Adb(this IDevice device,string command)
        {
            throw new NotImplementedException();
        }
        public static Tuple<Output, int> Fastboot(this IDevice device,string command)
        {
            throw new NotImplementedException();
        }
        public static void Reboot2Recovery(this IDevice device)
        {
            throw new NotImplementedException();
        }
        public static void Reboot2System(this IDevice device)
        {
            throw new NotImplementedException();
        }
        public static void Reboot2Fastboot(this IDevice device)
        {
            throw new NotImplementedException();
        }
        public static void Reboot29008(this IDevice device)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 检查是否有SU权限
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool HaveSU(this IDevice device)
        {
            var command = new SuCommand(device, "ls");
            return command.Execute().ExitCode == 0;
        }
        /// <summary>
        /// 获取Shell命令对象
        /// </summary>
        /// <param name="device"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static IProcessBasedCommand GetShellCommand(this IDevice device, string sh)
        {
            device.ThrowIfNotAlive();
            return new ShellCommand(device, sh);
        }
        public static IBroadcastSender GetBroadcastSender(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new BroadcastSender(device);
        }
        public static IBuildPropGetter GetBuildPropGetter(this IDevice device)
        {
            device.ThrowIfNotAlive();
            throw new NotImplementedException();
        }
        public static IActivityManager GetActivityManager(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new Management.AppFx.Impl.ActivityManager(device);
        }
        public static IPackageManager GetPackageManager(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new Management.AppFx.Impl.PackageManager(device);
        }
        public static IServiceManager GetServiceManager(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new Management.AppFx.Impl.ServiceManager(device);
        }

    }
}
