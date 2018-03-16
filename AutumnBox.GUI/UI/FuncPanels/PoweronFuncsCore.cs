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
            bool isInstall = PackageManager.IsInstall(tragetDevice, PkgName) == true;
            if (!isInstall)
            {
                BoxHelper.ShowMessageDialog("Warning", ErrorMsgKey);
            }
            return isInstall;
        }
    }
    public class DeviceOwnerSetterTipAttribute : FuncsPrecheckAttribute
    {
        public readonly string MsgKey;
        public DeviceOwnerSetterTipAttribute(string msg)
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
            Logger.Info(this,users.Length);
            if (users.Length > 0)
            {
                return BoxHelper.ShowChoiceDialog("Warning",
                  "msgMaybeHaveOtherUser",
                  "btnCancel", "btnIHaveDeletedAllUser").ToBool();
            }
            return true;
        }
    }
    public interface IPoweronFuncsCore
    {
        void ActivateBrevent(DeviceBasicInfo targetDevice);
        void ActivateIceBox(DeviceBasicInfo targetDevice);
    }
    public class PoweronFuncsCore : IPoweronFuncsCore
    {
        [InstallCheck("me.piebridge.brevent", ErrorMsgKey = "msgPlzInstallBreventFirst")]
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
        [DeviceOwnerSetterTip("msgIceAct")]
        [DeviceUserCheck]
        public void ActivateIceBox(DeviceBasicInfo targetDevice)
        {
            IceBoxActivator iceBoxActivator = new IceBoxActivator();
            iceBoxActivator.Init(new FlowArgs() { DevBasicInfo = targetDevice });
            iceBoxActivator.RunAsync();
            BoxHelper.ShowLoadingDialog(iceBoxActivator);
        }

    }
    public class PowerFuncsCoreAdvisor : IMethodInterceptor
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
