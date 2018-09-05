/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:12:14 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device
{
    partial class DeviceExtension
    {
        private const string ipPattern = @"(?<ip>\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})";
		/// <summary>
        /// 获取该设备在局域网中的IP
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static System.Net.IPAddress GetLanIP(this IDevice device)
        {
            var result = device.Shell("ifconfig wlan0");
            var match = System.Text.RegularExpressions.Regex.Match(result.ToString(), ipPattern);
            if (match.Success)
            {
                return System.Net.IPAddress.Parse(match.Result("${ip}"));
            }
            else
            {
                result = device.Shell("ifconfig netcfg");
                match = System.Text.RegularExpressions.Regex.Match(result.ToString(), ipPattern);
                if (match.Success)
                {
                    return System.Net.IPAddress.Parse(match.Result("${ip}"));
                }
            }
            return null;
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
        /// <summary>
        /// 获取广播发送器
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static IBroadcastSender GetBroadcastSender(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new BroadcastSender(device);
        }
        /// <summary>
        /// 获取build.prop信息获取器
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static IBuildPropGetter GetBuildPropGetter(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new DeviceBuildPropGetter(new DeviceSerialNumber(device.SerialNumber));
        }
        /// <summary>
        /// 获取Activity管理器
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static IActivityManager GetActivityManager(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new ActivityManager(device);
        }
        /// <summary>
        /// 获取包管理器
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static IPackageManager GetPackageManager(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new PackageManager(device);
        }
        /// <summary>
        /// 获取服务管理器
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static IServiceManager GetServiceManager(this IDevice device)
        {
            device.ThrowIfNotAlive();
            return new ServiceManager(device);
        }
    }
}
