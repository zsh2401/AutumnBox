/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 17:43:26 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Device.ActivityManager
{
    /// <summary>
    /// 广播管理器
    /// </summary>
    public static class Broadcast
    {
        /// <summary>
        /// 发送一个广播
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="broadcast">广播内容</param>
        /// <returns></returns>
        public static AdvanceOutput Send(DeviceSerialNumber device, string broadcast) {
           return ActivityManagerShared.Executer.QuicklyShell(device, $"am broadcast -a {broadcast}");
        }
    }
}
