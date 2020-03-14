using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// CommandExecutor的拓展方法们
    /// </summary>
    public static class CommandExecutorExtension
    {
        /// <summary>
        /// 异步执行CMD命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ICommandResult> CmdAsync(this ICommandExecutor executor, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Cmd(executor, args);
            });
        }
        /// <summary>
        /// 异步执行ADB SHELL命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ICommandResult> AdbShellAsync(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return AdbShell(executor, device, args);
            });
        }
        /// <summary>
        /// 异步执行针对Fastboot命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ICommandResult> FastbootAsync(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Fastboot(executor, device, args);
            });
        }
        /// <summary>
        /// 异步执行针对设备的ADB命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ICommandResult> AdbAsync(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Adb(executor, device, args);
            });
        }
        /// <summary>
        /// 异步执行Fastboot命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ICommandResult> FastbootAsync(this ICommandExecutor executor, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Fastboot(executor, args);
            });
        }
        /// <summary>
        /// 异步执行ADB命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task<ICommandResult> AdbAsync(this ICommandExecutor executor, params string[] args)
        {
            return await Task.Run(() =>
            {
                return Adb(executor, args);
            });
        }
        /// <summary>
        /// CMD
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandResult Cmd(this ICommandExecutor executor, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            string joined = string.Join(" ", args);
            string _args = $"/c {joined}";
            return executor.Execute("cmd.exe", _args);
        }
        /// <summary>
        /// 执行adb shell命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandResult AdbShell(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            string joined = string.Join(" ", args);
            return Adb(executor, device, $"shell {joined}");
        }
        /// <summary>
        /// 执行fastboot命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandResult Fastboot(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            FileInfo exe = ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-s {device.SerialNumber} {joined}";
            return executor.Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 执行针对设备的adb命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandResult Adb(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            if (!ManagedAdb.Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("adb server was killed");
            }
            FileInfo exe = ManagedAdb.Adb.AdbFile;
            string joined = string.Join(" ", args);
            string compCommand = $"-s {device.SerialNumber} {joined}";
            return executor.Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 执行非针对设备的fastboot命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandResult Fastboot(this ICommandExecutor executor, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            IAdbServer adbServer = ManagedAdb.Adb.Server;
            FileInfo exe = ManagedAdb.Adb.FastbootFile;
            string joined = string.Join(" ", args);
            string compCommand = $"{joined}";
            return executor.Execute(exe.FullName, compCommand);
        }
        /// <summary>
        /// 执行非针对设备的adb命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandResult Adb(this ICommandExecutor executor, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            if (!ManagedAdb.Adb.Server.IsEnable)
            {
                throw new InvalidOperationException("adb server is killed");
            }
            IAdbServer adbServer = ManagedAdb.Adb.Server;
            FileInfo exe = ManagedAdb.Adb.AdbFile;
            string joined = string.Join(" ", args);
            return executor.Execute(exe.FullName, joined);
        }
    }
}
