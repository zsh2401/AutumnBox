/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:39:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 服务管理器
    /// </summary>
    public sealed class ServiceManager : DeviceCommander
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public ServiceManager(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="cn"></param>
        /// <param name="intent"></param>
        public void Start(ComponentName cn, Intent intent = null)
        {
            Executor.AdbShell(Device,
                $"am startservice -n {cn.ToString()} {intent?.ToString()}")
                .ThrowIfShellExitCodeNotEqualsZero();
        }
    }
}
