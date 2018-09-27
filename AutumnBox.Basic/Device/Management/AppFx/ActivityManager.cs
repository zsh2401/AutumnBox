/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:49:54 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// Activity管理器
    /// </summary>
    public class ActivityManager : DeviceCommander, IReceiveOutputByTo<ActivityManager>
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public ActivityManager(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 启动一个Activity
        /// </summary>
        /// <param name="pkgName"></param>
        /// <param name="activityClassName"></param>
        /// <param name="intent"></param>
        public void StartActivity(string pkgName, string activityClassName, Intent intent = null)
        {
            CmdStation.GetShellCommand(Device,
                $"am start -n {pkgName}/.{activityClassName} {intent?.ToAdbArguments()}")
                .To(RaiseOutput)
                 .Execute()
                 .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 启动一个组件，例如某个activity
        /// </summary>
        /// <param name="componentName"></param>
        /// <param name="intent"></param>
        public void StartComponent(ComponentName componentName, Intent intent = null)
        {
            CmdStation.GetShellCommand(Device,
                 $"am start -n {componentName.ToString()} {intent?.ToAdbArguments()}")
                    .To(RaiseOutput)
                  .Execute()
                  .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 启动一个动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="intent"></param>
        public void StartAction(string action, Intent intent)
        {
            CmdStation.GetShellCommand(Device,
                $"am start -a {action} {intent?.ToAdbArguments()}")
                   .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 启动一个Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="intent"></param>
        public void StartCategory(string category, Intent intent)
        {
            CmdStation.GetShellCommand(Device,
                 $"am start -c {category} {intent?.ToAdbArguments()}")
                    .To(RaiseOutput)
                 .Execute()
                 .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 强制停止某个APP
        /// adb command:adb shell am force-stop com.qihoo360.mobilesafe
        /// </summary>
        /// <param name="pkgName"></param>
        public void ForceStop(string pkgName) {
            CmdStation.GetShellCommand(Device,
                $"am force-stop {pkgName}")
                   .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 发送收紧内存的命令
        /// adb command example:adb shell am send-trim-memory 12345 RUNNING_LOW
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="level"></param>
        public void TrimMemory(int pid, TrimMemoryLevel level) {
            CmdStation.GetShellCommand(Device,
                $"am send-trim-memory {pid} {level}")
                   .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 通过To模式订阅
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ActivityManager To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
