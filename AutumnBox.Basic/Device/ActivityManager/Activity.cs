/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 17:57:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Device.ActivityManager
{
    /// <summary>
    /// 活动管理器
    /// </summary>
    public static class Activity
    {
        /// <summary>
        /// 启动一个Activty
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="pkgName">包名</param>
        /// <param name="className">Activity的类名</param>
        /// <returns></returns>
        public static AdvanceOutput Start(DeviceSerialNumber device, string pkgName, string className) {
            return ActivityManagerShared.Executer.QuicklyShell(device,$"am start {pkgName}/.{className}");
        }
    }
}
