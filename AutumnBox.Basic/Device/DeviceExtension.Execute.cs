/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:34:37 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.Basic.Device
{
    /// <summary>
    /// 设备对象拓展
    /// </summary>
    public static partial class DeviceExtension
    {
        /// <summary>
        /// 执行shell命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static Tuple<Output, int> Shell(this IDevice device, string sh)
        {
            var cmd = new ShellCommand(device, sh);
            var result = cmd.Execute();
            return new Tuple<Output, int>(result.Output, result.ExitCode);
        }
        /// <summary>
        /// 执行ADB命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Tuple<Output, int> Adb(this IDevice device, string command)
        {
            var cmd = new AdbCommand(device, command);
            var result = cmd.Execute();
            return new Tuple<Output, int>(result.Output, result.ExitCode);
        }
        /// <summary>
        /// 执行Fastboot命令
        /// </summary>
        /// <param name="device"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Tuple<Output, int> Fastboot(this IDevice device, string command)
        {
            var cmd = new FastbootCommand(device, command);
            var result = cmd.Execute();
            return new Tuple<Output, int>(result.Output, result.ExitCode);
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
    }
}
