/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/30 0:35:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.OpenFramework.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Extension
{
    public abstract class StrictVisualExtension : AtmbVisualExtension
    {
        protected CommandStation CmdStation { get; private set; }
        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            CmdStation = new CommandStation();
        }
        protected override bool VisualStop()
        {
            CmdStation.Free();
            return true;
        }
        #region Command Getter
        protected AdbCommand GetDeviceAdbCommand(string cmd)
        {
            ThrowIfCanceled();
            return CmdStation.GetAdbCommand(TargetDevice, cmd);
        }
        protected AdbCommand GetNoDeviceAdbCommand(string cmd)
        {
            ThrowIfCanceled();
            return CmdStation.GetAdbCommand(cmd);
        }
        protected FastbootCommand GetDeviceFastbootCommand(string cmd)
        {
            ThrowIfCanceled();
            return CmdStation.GetFastbootCommand(TargetDevice, cmd);
        }
        protected FastbootCommand GetNoDeviceFastbootCommand(string cmd)
        {
            ThrowIfCanceled();
            return CmdStation.GetFastbootCommand(cmd);
        }
        protected WindowsCmdCommand GetWindowsCmdCommnad(string cmd)
        {
            ThrowIfCanceled();
            return CmdStation.GetCmdCommand(cmd);
        }
        #endregion
        #region Device Manager Getter
        protected ActivityManager GetActivitiyManager()
        {
            return new ActivityManager(TargetDevice)
            {
                CmdStation = this.CmdStation,
            };
        }
        protected PackageManager GetPackageManager()
        {
            return new PackageManager(TargetDevice)
            {
                CmdStation = this.CmdStation,
            };
        }
        protected WindowManager GetWindowManager()
        {
            return new WindowManager(TargetDevice)
            {
                CmdStation = this.CmdStation,
            };
        }
        protected ServiceManager GetServiceManager()
        {
            return new ServiceManager(TargetDevice)
            {
                CmdStation = this.CmdStation,
            };
        }
        #endregion
    }
}
