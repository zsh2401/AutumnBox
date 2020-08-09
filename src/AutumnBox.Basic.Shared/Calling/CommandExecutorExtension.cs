using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// CommandExecutor的一系列实用拓展方法
    /// </summary>
    public static class CommandExecutorExtension
    {
        /// <summary>
        /// 异步执行CMD命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static async Task<CommandResult> CmdAsync(this ICommandExecutor executor, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static async Task<CommandResult> AdbShellAsync(this ICommandExecutor executor, IDevice device, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static async Task<CommandResult> FastbootAsync(this ICommandExecutor executor, IDevice device, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static async Task<CommandResult> AdbAsync(this ICommandExecutor executor, IDevice device, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static async Task<CommandResult> FastbootAsync(this ICommandExecutor executor, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static async Task<CommandResult> AdbAsync(this ICommandExecutor executor, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static CommandResult Cmd(this ICommandExecutor executor, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static CommandResult AdbShell(this ICommandExecutor executor, IDevice device, params string[] args)
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
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static CommandResult Fastboot(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            string joined = string.Join(" ", args);
            string compCommand = $"-s {device.SerialNumber} {joined}";
            return executor.Execute("fastboot.exe", compCommand);
        }

        /// <summary>
        /// 执行针对设备的adb命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="device"></param>
        /// <param name="args"></param>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <returns></returns>
        public static CommandResult Adb(this ICommandExecutor executor, IDevice device, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            string compCommand = $"-s {device.SerialNumber} {string.Join(" ", args)}";
            return executor.Execute("adb.exe", compCommand);
        }

        /// <summary>
        /// 执行非针对设备的fastboot命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <returns></returns>
        public static CommandResult Fastboot(this ICommandExecutor executor, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            string joined = string.Join(" ", args);
            string compCommand = $"{joined}";
            return executor.Execute("fastboot.exe", compCommand);
        }

        /// <summary>
        /// 执行非针对设备的adb命令
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <exception cref="CommandCancelledException">命令被外部中断</exception>
        /// <exception cref="ArgumentNullException">参数为空</exception>
        /// <returns></returns>
        public static CommandResult Adb(this ICommandExecutor executor, params string[] args)
        {
            if (executor == null)
            {
                throw new ArgumentNullException(nameof(executor));
            }

            string joined = string.Join(" ", args);
            return executor.Execute("adb.exe", joined);
        }
    }
}
