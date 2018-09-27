/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 23:45:05
** filename: DeviceBuildPropSetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// build.prop 设置器,通常需要root权限
    /// </summary>
    public class DeviceBuildPropSetter : DeviceCommander,Data.IReceiveOutputByTo<DeviceBuildPropSetter>
    {
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="device"></param>
        /// <exception cref="Exceptions.DeviceHasNoSuException">当设备没有su权限时抛出</exception>
        /// <exception cref="Exceptions.CommandNotFoundException">设备不支持setprop时抛出</exception>
        public DeviceBuildPropSetter(IDevice device) : base(device)
        {
            if (!device.HaveSU())
            {
                throw new Exceptions.DeviceHasNoSuException();
            }
            ShellCommandHelper.CommandExistsCheck(device, "setprop");
        }
        /// <summary>
        ///设置 build.prop
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <exception cref="Exceptions.AdbShellCommandFailedException"></exception>
        public void Set(string key, string value)
        {
            if (!Device.HaveSU())
            {
                throw new DeviceHasNoSuException();
            }
            CmdStation.GetShellCommand(Device,
                $"setprop {key} {value}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        /// <summary>
        /// 通过To模式订阅
        /// </summary>
        /// <param name="callback"></param>
        /// <returns></returns>
        public DeviceBuildPropSetter To(Action<OutputReceivedEventArgs> callback)
        {
            RegisterToCallback(callback);
            return this;
        }
    }
}
