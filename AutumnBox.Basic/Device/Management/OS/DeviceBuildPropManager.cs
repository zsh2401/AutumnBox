/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:02:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Device.Management.OS
{
    public class DeviceBuildPropManager : DeviceCommander
    {
        public DeviceBuildPropManager(IDevice device) : base(device)
        {
        }
        public virtual string this[string key]
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
        public virtual string Get(string key)
        {
            var result = CmdStation.GetShellCommand(Device,
                $"getprop {key}")
                .To(RaiseOutput)
                .Execute();
            return result.ExitCode == 0 ? result.Output.ToString() : null;
        }
        public virtual void Set(string key, string value)
        {
            SettingCheck();
            CmdStation.GetShellCommand(Device,
                $"setprop {key} {value}")
                .To(RaiseOutput)
                .Execute()
                .ThrowIfShellExitCodeNotEqualsZero();
        }
        protected virtual void SettingCheck()
        {
            if (!Device.HaveSU())
            {
                throw new Exceptions.DeviceHasNoSuException();
            }
        }
        protected virtual void GettingCheck() { }
    }
}
