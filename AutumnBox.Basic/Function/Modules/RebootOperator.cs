/* =============================================================================*\
*
* Filename: RebootOperator.cs
* Description: 
*
* Version: 1.0
* Created: 9/5/2017 18:24:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Bundles;
using AutumnBox.Basic.Function.Event;

namespace AutumnBox.Basic.Function.Modules
{
    /// <summary>
    /// 手机重启器
    /// </summary>
    public class RebootOperator : FunctionModule
    {
        enum ExecuterType
        {
            Adb,
            Fastboot,
        }
        private ExecuterType t;
        private RebootArgs _Args;
        protected override void Create(BundleForCreate bundle)
        {
            base.Create(bundle);
            this._Args = (RebootArgs)bundle.Args;
            switch (_Args.nowStatus)
            {
                case DeviceState.Fastboot:
                    t = ExecuterType.Fastboot;
                    break;
                default:
                    t = ExecuterType.Adb;
                    break;
            }
        }
        protected override CheckResult Check(ModuleArgs args)
        {
            bool isRebootToRecoveryOnBootloader = (_Args.nowStatus == DeviceState.Fastboot && _Args.rebootOption == RebootOptions.Recovery);
            if (isRebootToRecoveryOnBootloader) return CheckResult.DeviceStatusError;
            else return CheckResult.OK;
        }
        protected override OutputData MainMethod(BundleForTools toolsBundle)
        {
            string command = "reboot";
            if (_Args.rebootOption == RebootOptions.Bootloader)
            {
                command = "reboot-bootloader";
            }
            else if (_Args.rebootOption == RebootOptions.System)
            {
                command = "reboot";
            }
            else if (_Args.rebootOption == RebootOptions.Recovery)
            {
                command = "reboot recovery";
            }
            else if (_Args.rebootOption == RebootOptions.Snapdragon9008 && _Args.nowStatus != DeviceState.Fastboot)
            {
                command = "reboot edl";
            }
            else if (_Args.rebootOption == RebootOptions.Snapdragon9008 && _Args.nowStatus == DeviceState.Fastboot)
            {
                command = "reboot-edl";
            }
            OutputData o;
            switch (t)
            {
                case ExecuterType.Adb:
                    o = toolsBundle.Ae(command);
                    break;
                default:
                    o = toolsBundle.Fe(command);
                    break;
            }
            return o;
        }
    }
}
