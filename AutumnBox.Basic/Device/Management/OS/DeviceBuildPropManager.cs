/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:02:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// build.prop管理器
    /// </summary>
    public sealed class DeviceBuildPropManager : DeviceCommander, Data.IReceiveOutputByTo<DeviceBuildPropManager>
    {
        /// <summary>
        /// 获取器
        /// </summary>
        private DeviceBuildPropGetter Getter
        {
            get
            {
                if (_getter == null)
                {
                    _getter = new DeviceBuildPropGetter(Device);
                }
                return _getter;
            }
            set
            {
                _getter = value;
                _getter.To(RaiseOutput);
            }
        }
        private DeviceBuildPropGetter _getter;
        /// <summary>
        /// 设置器
        /// </summary>
        private DeviceBuildPropSetter Setter
        {
            get
            {
                if (_setter == null)
                {
                    _setter = new DeviceBuildPropSetter(Device);
                }
                return _setter;
            }
            set
            {
                _setter = value;
                _setter.To(RaiseOutput);
            }
        }
        private DeviceBuildPropSetter _setter;
        /// <summary>
        /// 构造管理器
        /// </summary>
        /// <param name="device"></param>
        public DeviceBuildPropManager(IDevice device) : base(device)
        {
            //ShellCommandHelper.CommandExistsCheck(device,"getprop");
            //ShellCommandHelper.CommandExistsCheck(device,"setprop");
        }
        /// <summary>
        /// 获取或设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public string this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, value);
            }
        }
        /// <summary>
        /// 获取键值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public string Get(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }
            Getter.CmdStation = this.CmdStation;
            return Getter.Get(key);
        }
        /// <summary>
        /// 设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void Set(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("message", nameof(key));
            }

            Setter.CmdStation = this.CmdStation;
            Setter.Set(key, value);
        }
        /// <summary>
        /// 通过To模式订阅输出
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public DeviceBuildPropManager To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
