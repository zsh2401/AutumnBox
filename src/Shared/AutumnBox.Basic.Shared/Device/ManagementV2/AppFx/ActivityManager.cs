#nullable enable
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Data;
using System;

namespace AutumnBox.Basic.Device.ManagementV2.AppFx
{
    /// <summary>
    /// ActivityManager 封装android shell的am命令
    /// </summary>
    public class ActivityManager
    {
        readonly IDevice device;
        readonly ICommandExecutor executor;

        /// <summary>
        /// 构造AM
        /// </summary>
        /// <param name="device"></param>
        /// <param name="executor"></param>
        public ActivityManager(IDevice device, ICommandExecutor executor)
        {
            this.device = device ?? throw new ArgumentNullException(nameof(device));
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        /// <summary>
        /// 启动一个Activity
        /// </summary>
        /// <param name="pkgName"></param>
        /// <param name="activityClassName"></param>
        /// <param name="intent"></param>
        public CommandResult StartActivity(string pkgName, string activityClassName, Intent intent = null)
        {
            return executor.AdbShell(device, $"am start {pkgName}/.{activityClassName} {intent?.ToAdbArguments()}");
        }

        /// <summary>
        /// 启动一个Activity
        /// </summary>
        /// <param name="componentNameString">组件名字符串</param>
        /// <param name="intent"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public CommandResult StartActivity(string componentNameString, Intent intent = null)
        {
            return executor.AdbShell(device, $"am start -n {componentNameString} {intent?.ToAdbArguments()}");
        }

        /// <summary>
        /// 启动一个组件，例如某个activity
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="intent"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public CommandResult StartComponent(ComponentName componentName, Intent intent = null)
        {
            return executor.AdbShell(device, $"am start -n {componentName.ToString()} {intent?.ToAdbArguments()}");
        }

        /// <summary>
        /// 启动一个动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="intent"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public CommandResult StartAction(string action, Intent intent)
        {
            return executor.AdbShell(device, $"am start -a {action} {intent?.ToAdbArguments()}");
        }

        /// <summary>
        /// 启动一个Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="intent"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public CommandResult StartCategory(string category, Intent intent)
        {
            return executor.AdbShell(device, $"am start -c {category} {intent?.ToAdbArguments()}");
        }

        /// <summary>
        /// 强制停止某个APP
        /// adb command:adb shell am force-stop com.qihoo360.mobilesafe
        /// </summary>
        /// <param name="pkgName"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public CommandResult ForceStop(string pkgName)
        {
            return executor.AdbShell(device, $"am force-stop {pkgName}");
        }

        /// <summary>
        /// 发送收紧内存的命令
        /// adb command example:adb shell am send-trim-memory 12345 RUNNING_LOW
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="level"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public CommandResult TrimMemory(int pid, TrimMemoryLevel level)
        {
            return executor.AdbShell(device, $"am send-trim-memory {pid} {level}");
        }
    }
}
