/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:39:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 服务管理器
    /// </summary>
    public class ServiceManager : DependOnDeviceObject
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="device"></param>
        public ServiceManager(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 启动一个服务
        /// </summary>
        /// <param name="pkgName"></param>
        /// <param name="className"></param>
        public void StartService(string pkgName, string className)
        {
            new ShellCommand(Device, $"am startservice {pkgName}/.{className}")
                .Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
