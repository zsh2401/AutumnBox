/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:35:07 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 广播发送器
    /// </summary>
    public sealed class BroadcastSender : DeviceCommander
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
        /// <param name="broadcast">广播内容</param>
        /// <param name="intent"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void Send(string broadcast, Intent intent = null)
        {
            Executor.AdbShell(Device,
                  $"am broadcast -a {broadcast} {intent?.ToAdbArguments()}")
                  .ThrowIfExitCodeNotEqualsZero(); ;
        }
    }
}
