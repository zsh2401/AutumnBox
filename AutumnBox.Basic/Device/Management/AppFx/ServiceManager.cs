/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:39:08 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 服务管理器
    /// </summary>
    public sealed class ServiceManager : DeviceCommander, Data.IReceiveOutputByTo<ServiceManager>
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
            CmdStation.GetShellCommand(Device,
                $"am startservice -n {cn.ToString()} {intent?.ToString()}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 通过To模式订阅输出事件
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public ServiceManager To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
