using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

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
        /// 异步执行FastbootWithRetry
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <param name="device"></param>
        /// <param name="retryTimes"></param>
        /// <param name="maxTimeout"></param>
        /// <exception cref="TimeoutException">超过重试次数</exception>
        /// <exception cref="ArgumentException">参数不合理</exception>
        /// <returns></returns>
        public static Task<CommandResult> FastbootWithRetryAsync(this ICommandExecutor executor, string args
            , IDevice device, int retryTimes = 10, int maxTimeout = 300)
        {
            return Task.Run(() =>
            {
                return FastbootWithRetry(executor, args, device, retryTimes, maxTimeout);
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
        /// 带有超时重试功能的fastboot指令执行器
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="args"></param>
        /// <param name="retryTimes"></param>
        /// <param name="maxTimeout"></param>
        /// <exception cref="TimeoutException">超过重试次数</exception>
        /// <exception cref="ArgumentException">参数不合理</exception>
        /// <returns></returns>
        public static CommandResult FastbootWithRetry(this ICommandExecutor executor, string args,
            IDevice? device = null, int retryTimes = 10, int maxTimeout = 300)
        {
            if (retryTimes * maxTimeout <= 0)
            {
                throw new ArgumentException("retryTimes and maxTimeout should not be smaller than 0");
            }
            const int INTERVAL_BETWEEN_PER_RETRY = 20;
            /*
            * 由于某些原因,部分设备上的fastboot指令会有玄学问题
            *因此此处实现了一个重试机制,尽可能获取到正确的值
            */
            CommandResult? result = null;
            for (int crtTime = 1; crtTime <= retryTimes; crtTime++)
            {
                CommandResult routine()
                {
                    return device == null ? executor.Fastboot(args) : executor.Fastboot(device, args);
                }
                Task.Run(() =>
                {
                    result = routine();
                });

                Thread.Sleep((int)maxTimeout);

                if (result != null && !result.Output.Contains("GetVar Variable Not found"))
                {
                    break;
                }
                else
                {
                    executor.CancelCurrent();
                    Thread.Sleep(INTERVAL_BETWEEN_PER_RETRY);
                }
            }
            if (result == null)
            {
                throw new TimeoutException();
            }
            return result;
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
