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

        #region Device Manager Getter
        protected ActivityManager ActivityManager
        {
            get
            {
                return _am.Value;
            }
        }
        private Lazy<ActivityManager> _am;

        protected DevicePolicyManager Dpm
        {
            get
            {
                return _dpm.Value;
            }
        }
        private Lazy<DevicePolicyManager> _dpm;

        protected PackageManager PackageManager
        {
            get
            {
                return _pm.Value;
            }
        }
        private Lazy<PackageManager> _pm;

        protected WindowManager WindowManager
        {
            get
            {
                return _wm.Value;
            }
        }
        private Lazy<WindowManager> _wm;

        protected ServiceManager ServiceManager
        {
            get
            {
                return _sm.Value;
            }
        }
        private Lazy<ServiceManager> _sm;

        private void InitLazyFactory()
        {
            _am = new Lazy<ActivityManager>(() => new ActivityManager(TargetDevice)
            {
                CmdStation = CmdStation,
            });
            _dpm = new Lazy<DevicePolicyManager>(() => new DevicePolicyManager(TargetDevice)
            {
                CmdStation = CmdStation,
            });
            _pm = new Lazy<PackageManager>(() => new PackageManager(TargetDevice)
            {
                CmdStation = CmdStation,
            });
            _wm = new Lazy<WindowManager>(() => new WindowManager(TargetDevice)
            {
                CmdStation = CmdStation,
            });
            _sm = new Lazy<ServiceManager>(() => new ServiceManager(TargetDevice)
            {
                CmdStation = CmdStation,
            });
        }
        #endregion
    }
}
