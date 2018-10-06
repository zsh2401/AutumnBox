/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/30 0:35:59 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Calling.Cmd;
using AutumnBox.Basic.Calling.Fastboot;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.Basic.Util;
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
            InitLazyFactory();
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

        #region Device Commander Getter
        protected TDevCommander GetDeviceCommander<TDevCommander>()
            where TDevCommander : DeviceCommander
        {
            return DevCmderFcty.GetDeviceCommander<TDevCommander>();
        }

        protected ActivityManager ActivityManager => GetDeviceCommander<ActivityManager>();

        protected DevicePolicyManager Dpm => GetDeviceCommander<DevicePolicyManager>();

        protected PackageManager PackageManager => GetDeviceCommander<PackageManager>();

        protected WindowManager WindowManager => GetDeviceCommander<WindowManager>();

        protected ServiceManager ServiceManager => GetDeviceCommander<ServiceManager>();

        protected DeviceBuildPropManager BuildProp => GetDeviceCommander<DeviceBuildPropManager>();

        protected BroadcastSender BroadcastSender => GetDeviceCommander<BroadcastSender>();

        protected Inputer Inputer => GetDeviceCommander<Inputer>();

        protected ScreenCapture ScreenCapture => GetDeviceCommander<ScreenCapture>();



        private DeviceCommanderFactory DevCmderFcty => _deviceCommanderFactory.Value;
        private Lazy<DeviceCommanderFactory> _deviceCommanderFactory;
        private void InitLazyFactory()
        {
            _deviceCommanderFactory = new Lazy<DeviceCommanderFactory>(() =>
            {
                return new DeviceCommanderFactory(this.TargetDevice, this.CmdStation);
            });
        }
        #endregion
    }
}
