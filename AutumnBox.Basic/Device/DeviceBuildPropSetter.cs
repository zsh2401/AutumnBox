/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 23:45:05
** filename: DeviceBuildPropSetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public class DeviceBuildPropSetter:IDisposable
    {
        private readonly AndroidShell shellAsSu;
        public DeviceBuildPropSetter(Serial serial)
        {
            shellAsSu = new AndroidShell(serial);
            shellAsSu.Connect();
            if (!shellAsSu.Switch2Su())
            {
                throw new DeviceHaveNoRootException();
            }
        }

        public void Dispose()
        {
            shellAsSu.Dispose();
        }

        public ShellOutput Set(string key, string value)
        {
            return shellAsSu.SafetyInput($"setprop {key} {value}");
        }
    }
}
