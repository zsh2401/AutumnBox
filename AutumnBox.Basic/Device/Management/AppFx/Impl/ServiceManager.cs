/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/31 9:39:08 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.AppFx.Impl
{
    class ServiceManager : DependOnDeviceObject, IServiceManager
    {
        public ServiceManager(IDevice device) : base(device)
        {
        }

        public void StartService(string pkgName, string className)
        {
            new AdbCommandBuilder().Device(Device)
                .Shell().
                Arg("am")
                .Arg("startservice")
                .Arg($"{pkgName}/.{className}").ToCommand().Execute()
                .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
