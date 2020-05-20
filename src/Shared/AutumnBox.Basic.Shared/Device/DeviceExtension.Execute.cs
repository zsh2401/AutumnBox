/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:34:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备对象拓展
    /// </summary>
    public static partial class DeviceExtension
    {
        private static ICommandProcedureManager CPM => BasicBooter.CommandProcedureManager;

        /// <summary>
        /// 以SU权限执行Shell命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="sh"></param>
        /// <param name="suCheck"></param>
        /// <exception cref="Exceptions.DeviceHasNoSuException"></exception>
        /// <returns></returns>
        public static CommandResult Su(this IDevice device, string sh, bool suCheck = true)
        {
            if (suCheck)
            {
                device.ThrowIfHaveNoSu();
            }
            using var cmd = CPM.OpenShellCommand(device, "su -c", sh);
            return cmd.Execute();
        }

        /// <summary>
        /// 执行shell命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static CommandResult Shell(this IDevice device, string sh)
        {
            using var cmd = CPM.OpenShellCommand(device, sh);
            return cmd.Execute();
        }

        /// <summary>
        /// 执行ADB命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandResult Adb(this IDevice device, string command)
        {
            var cmd = CPM.OpenADBCommand(device, command);
            return cmd.Execute();
        }

        /// <summary>
        /// 执行Fastboot命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandResult Fastboot(this IDevice device, string command)
        {
            using var cmd = CPM.OpenFastbootCommand(device, command);
            return cmd.Execute();
        }
        /// <summary>
        /// 根据设备状态，判断使用adb还是fastboot执行命令
        /// 当设备处于Fastboot状态时，使用fastboot执行，否则用adb执行
        /// </summary>
        /// <param name="device"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static CommandResult Auto(this IDevice device, string command)
        {
            if (device.State == DeviceState.Fastboot)
            {
                return device.Fastboot(command);
            }
            else
            {
                return device.Adb(command);
            }
        }
    }
}
