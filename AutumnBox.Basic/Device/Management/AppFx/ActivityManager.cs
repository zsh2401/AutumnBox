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
    public class ActivityManager : DeviceCommander,IReceiveOutputByTo<ActivityManager>
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
            var cmd = CmdStation.GetShellCommand(Device,
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
            var cmd = new ShellCommand(Device,
                $"am start -n {componentName.ToString()} {intent?.ToAdbArguments()}")
                   .To(RaiseOutput)
                 .Execute()
                 .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 启动一个动作
        /// </summary>
        /// <param name="action"></param>
        /// <param name="intent"></param>
        public void StartAction(string action, Intent intent)
        {
            var cmd = new ShellCommand(Device,
                $"am start -a {action} {intent?.ToAdbArguments()}")
                   .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 启动一个Category
        /// </summary>
        /// <param name="category"></param>
        /// <param name="intent"></param>
        public void StartCategory(string category, Intent intent)
        {
            var cmd = new ShellCommand(Device,
                $"am start -c {category} {intent?.ToAdbArguments()}")
                   .To(RaiseOutput)
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
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
