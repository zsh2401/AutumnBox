/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/16 11:14:02 (UTC +8:00)
** desc： ...
*************************************************/
using AopAlliance.Intercept;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.PackageManage;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI.UI.FuncPanels
{
    public abstract class FuncsPrecheckAttribute : Attribute
    {
        public abstract bool Check(DeviceBasicInfo tragetDevice);
    }
    public class InstallCheckAttribute : FuncsPrecheckAttribute
    {
        public string ErrorMsgKey { get; set; } = "PlzInstallAppFirst";
        public string PkgName { get; private set; }
        public InstallCheckAttribute(string pkgName)
        {
            this.PkgName = pkgName;
        }
        public override bool Check(DeviceBasicInfo tragetDevice)
        {
            Logger.Info(this, "install checking");
            bool isInstall = false;
            Task.Run(() =>
            {
                Thread.Sleep(500);
                isInstall = PackageManager.IsInstall(tragetDevice, PkgName) == true;
                BoxHelper.CloseLoadingDialog();
            });
            BoxHelper.ShowLoadingDialog();
            if (!isInstall)
            {
                BoxHelper.ShowMessageDialog("Warning", ErrorMsgKey);
            }
            return isInstall;
        }
    }
    public class TipAttribute : FuncsPrecheckAttribute
    {
        public readonly string MsgKey;
        public TipAttribute(string msg)
        {
            this.MsgKey = msg;
        }

        public override bool Check(DeviceBasicInfo tragetDevice)
        {
            return BoxHelper.ShowChoiceDialog("Notice", MsgKey).ToBool();
        }
    }
    public class DeviceUserCheckAttribute : FuncsPrecheckAttribute
    {
        public override bool Check(DeviceBasicInfo targetDevice)
        {
            var users = new UserManager(targetDevice).GetUsers();
            Logger.Info(this, users.Length);
            if (users.Length > 0)
            {
                return BoxHelper.ShowChoiceDialog("Warning",
                  "msgMaybeHaveOtherUser",
                  "btnCancel", "btnIHaveDeletedAllUser").ToBool();
            }
            return true;
        }
    }
    public interface IAdbActivatorsUI
    {
        void ActivateBrevent(DeviceBasicInfo targetDevice);
        void ActivateIceBox(DeviceBasicInfo targetDevice);
        void ActivateAirForzen(DeviceBasicInfo targetDevice);
        void ActivateShizukuManager(DeviceBasicInfo targetDevice);
        void ActivateIsland(DeviceBasicInfo targetDevice);
        void ActivateGeekMemoryCleaner(DeviceBasicInfo targetDevice);
        void ActivateStopapp(DeviceBasicInfo targetDevice);
        void ActivateBlackHole(DeviceBasicInfo targetDevice);
        void ActivateAnzenbokusu(DeviceBasicInfo targetDevice);
        void ActivateAnzenbokusuFake(DeviceBasicInfo targetDevice);
        void ActivateFreezeYou(DeviceBasicInfo targetDevice);
        void ActivateGreenifyAggressiveDoze(DeviceBasicInfo targetDevice);
    }
    public class AdbActivatorsUI : IAdbActivatorsUI
    {
        [InstallCheck(BreventServiceActivator._AppPackageName, ErrorMsgKey = "msgPlzInstallBreventFirst")]
        public void ActivateBrevent(DeviceBasicInfo targetDevice)
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(targetDevice.Serial).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.BtnCancel:
                        return;
                    case ChoiceResult.BtnLeft:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.BtnRight:
                        fixAndroidO = true;
                        break;
                }
            }
            var args = new ShScriptExecuterArgs() { DevBasicInfo = targetDevice, FixAndroidOAdb = fixAndroidO };
            /*开始操作*/
            BreventServiceActivator activator = new BreventServiceActivator();
            activator.Init(args);
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(IceBoxActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallIceBoxFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateIceBox(DeviceBasicInfo targetDevice)
        {
            IceBoxActivator iceBoxActivator = new IceBoxActivator();
            iceBoxActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            iceBoxActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(iceBoxActivator);
        }

        [InstallCheck(AirForzenActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAirForzenFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateAirForzen(DeviceBasicInfo targetDevice)
        {
            AirForzenActivator airForzenActivator = new AirForzenActivator();
            airForzenActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            airForzenActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(airForzenActivator);
        }

        [InstallCheck(ShizukuManagerActivator._AppPackageName, ErrorMsgKey = "msgPlsInstallShizukuManagerFirst")]
        public void ActivateShizukuManager(DeviceBasicInfo targetDevice)
        {
            bool fixAndroidO = false;
            if (new DeviceBuildPropGetter(targetDevice.Serial).GetAndroidVersion() >= new Version("8.0.0"))
            {
                var result = BoxHelper.ShowChoiceDialog("msgNotice", "msgFixAndroidO", "btnDoNotOpen", "btnOpen");
                switch (result)
                {
                    case ChoiceResult.BtnCancel:
                        return;
                    case ChoiceResult.BtnLeft:
                        fixAndroidO = false;
                        break;
                    case ChoiceResult.BtnRight:
                        fixAndroidO = true;
                        break;
                }
            }
            var args = new ShScriptExecuterArgs() { DevBasicInfo = targetDevice, FixAndroidOAdb = fixAndroidO };
            /*开始操作*/
            ShizukuManagerActivator activator = new ShizukuManagerActivator();
            activator.Init(args);
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(IslandActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAirForzenFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateIsland(DeviceBasicInfo targetDevice)
        {
            IslandActivator islandActivator = new IslandActivator();
            islandActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            islandActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(islandActivator);
        }

        [InstallCheck(GeekMemoryCleanerActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallGMCFirst")]
        [Tip("msgActiveGMC")]
        public void ActivateGeekMemoryCleaner(DeviceBasicInfo targetDevice)
        {

            var activator = new GeekMemoryCleanerActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);

        }

        [InstallCheck(StopAppActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallStopAppFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateStopapp(DeviceBasicInfo targetDevice)
        {
            StopAppActivator activator = new StopAppActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(BlackHoleActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallBlackholeFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateBlackHole(DeviceBasicInfo targetDevice)
        {
            BlackHoleActivator activator = new BlackHoleActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(AnzenbokusuActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAnzenbokusuFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateAnzenbokusu(DeviceBasicInfo targetDevice)
        {
            AnzenbokusuActivator activator = new AnzenbokusuActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(AnzenbokusuFakeActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallAnzenbokusuFakeFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateAnzenbokusuFake(DeviceBasicInfo targetDevice)
        {
            AnzenbokusuFakeActivator activator = new AnzenbokusuFakeActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(FreezeYouActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallFreezeYouFirst")]
        [Tip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateFreezeYou(DeviceBasicInfo targetDevice)
        {
            FreezeYouActivator activator = new FreezeYouActivator();
            activator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }

        [InstallCheck(GreenifyAggressiveDozeActivator.AppPackageName, ErrorMsgKey = "msgPlsInstallGreenifyFirst")]
        public void ActivateGreenifyAggressiveDoze(DeviceBasicInfo targetDevice)
        {
            var activator = new GreenifyAggressiveDozeActivator();
            activator.Init(new FlowArgs()
            {
                DevBasicInfo = targetDevice
            });
            activator.RunAsync();
            BoxHelper.ShowLoadingDialog(activator);
        }
    }
    public class AdbActivatorsUIAdvisor : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            var devInfo = (DeviceBasicInfo)invocation.Arguments[0];
            foreach (var prechecker in GetPrechecker(invocation.Method))
            {
                if (!prechecker.Check(devInfo))
                {
                    return null;
                }
            }
            return invocation.Proceed();
        }

        private static FuncsPrecheckAttribute[] GetPrechecker(MethodInfo method)
        {
            var attrs = method.GetCustomAttributes(true);
            List<FuncsPrecheckAttribute> precheckers = new List<FuncsPrecheckAttribute>();
            foreach (object attr in attrs)
            {
                if (attr is FuncsPrecheckAttribute result)
                {
                    precheckers.Add(result);
                }
            }
            return precheckers.ToArray();
        }
    }
}
