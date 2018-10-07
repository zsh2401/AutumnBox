/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:14:43 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.Device.Management.AppFx
{
    /// <summary>
    /// 没错,这就是DPM!
    /// </summary>
    public sealed class DevicePolicyManager : DeviceCommander,Data.IReceiveOutputByTo<DevicePolicyManager>
    {
        /// <summary>
        /// 构造DPM实例
        /// </summary>
        /// <param name="device"></param>
        public DevicePolicyManager(IDevice device) : base(device)
        {
        }
        /// <summary>
        /// 设置ActiveAdmin
        /// </summary>
        /// <param name="cn">组件名</param>
        /// <param name="uid">UID,不填则将对全部用户起效</param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void SetActiveAdmin(ComponentName cn, int? uid = null)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置ActiveAdmin
        /// </summary>
        /// <param name="cn">组件名</param>
        /// <param name="uid">UID,不填则将对全部用户起效</param>
        /// <param name="name">易读的别名(the human-readable organization name)</param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void SetProfileOwner(ComponentName cn, int? uid = null, string name = null)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 设置Device Owner
        /// </summary>
        /// <param name="cn">组件名</param>
        /// <param name="uid">UID,不填则将对全部用户起效</param>
        /// <param name="name">易读的别名(the human-readable organization name)</param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void SetDeviceOwner(ComponentName cn, int? uid = null, string name = null)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 移除ActiveAdmin
        /// </summary>
        /// <param name="cn">组件名</param>
        /// <param name="uid">UID,不填则将对全部用户起效</param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void RemoveActiveAdmin(ComponentName cn, int? uid = null)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 通过To模式订阅输出
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public DevicePolicyManager To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
