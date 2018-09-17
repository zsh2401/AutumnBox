/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 5:49:54 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// Activity管理器
    /// </summary>
    public class ActivityManager : DependOnDeviceObject
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
        public void StartActivity(string pkgName, string activityClassName)
        {
            var cmd = new ShellCommand(Device, $"am start {pkgName}/.{activityClassName}")
                 .Execute()
                 .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
