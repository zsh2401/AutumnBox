/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:35:07 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 广播发送器
    /// </summary>
    public class BroadcastSender : DependOnDeviceObject
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public BroadcastSender(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 发送一个广播
        /// </summary>
        /// <param name="broadcast"></param>
        public void Send(string broadcast)
        {
            new ShellCommand(Device, $"am broadcast -a {broadcast}")
                .Execute()
                .ThrowIfExitCodeNotEqualsZero(); ;
        }
    }
}
