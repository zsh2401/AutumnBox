/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.Basic.Exceptions;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.CoreModules.Attribute;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Running;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.CoreModules.Lib
{
    [ExtDesc("使用奇淫技巧暴力设置设备管理员,\n注意:使用此模块前,必须先移除屏幕锁,指纹锁等,否则将可能导致不可预见的后果", "en-us:Use the sneaky skills to set up the device administrator, \n Note: Before using this module, you must first remove the screen lock, fingerprint lock, etc., otherwise it may lead to unforeseen consequences")]
    [UserAgree("EGodPowerWarning")]
    [UserAgree("EGodPowerMsgDeleteLockFirst")]
    [UserAgree("EGodPowerWarningAgain")]
    [ExtIcon("Icons.nuclear.png")]
    [DpmReceiver(null)]
    internal abstract class EDpmSetterBase : OfficialVisualExtension
    {
        private string _cn;
        private CstmDpmCommander dpmCommander;

        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            ClassExtensionScanner scanner = new ClassExtensionScanner(GetType());
            scanner.Scan(ClassExtensionScanner.ScanOption.Informations);
            var infos = scanner.Informations;
            _cn = infos[DpmReceiverAttribute.KEY].Value as string;
            RunOnUIThread(() =>
            {
                var viewSize = ViewSize;
                viewSize.Height += 100;
                viewSize.Width += 100;
                ViewSize = viewSize;
            });
            EnableHelpButton(() =>
            {
                try
                {
                    System.Diagnostics.Process.Start("http://www.atmb.top/go/help/dpmhelp");
                }
                catch (Exception e)
                {
                    Logger.Warn("cannot go to dpm setter's help", e);
                }
            });
        }

        protected sealed override int VisualMain()
        {
            WriteInitInfo();
            dpmCommander = new CstmDpmCommander(this, TargetDevice)
            {
                CmdStation = this.CmdStation
            };
            dpmCommander.To(OutputPrinter);
            dpmCommander.Extract();
            Progress = 20;

            WriteLineAndSetTip(Res("EGodPowerExtractingApk"));
            dpmCommander.Extract();
            Progress = 40;

            WriteLineAndSetTip(Res("EGodPowerPushingApk"));
            dpmCommander.PushToDevice();

            Progress = 50;

            WriteLineAndSetTip(Res("EGodPowerRmUser"));
            dpmCommander.RemoveUsers();

            Progress = 60;

            WriteLineAndSetTip(Res("EGodPowerRmAcc"));
            dpmCommander.RemoveAccounts();

            Progress = 70;
            WriteLineAndSetTip(Res("EGodPowerSettingDPM"));

            Progress = 80;
            try
            {
                return SetDpmWithDpm() ? OK : ERR;
            }
            catch (CommandNotFoundException)
            {
                if (Ux.DoYN(Res("EGodPowerUseDpmPro")))
                {
                    return SetDpmWithDpmPro(dpmCommander) ? OK : ERR;
                }
                else
                {
                    return ERR;
                }
            }
        }

        protected virtual bool SetDpmWithDpm()
        {
            WriteLine("using dpm");
            var dpm = new DevicePolicyManager(DeviceSelectedOnCreating)
            {
                CmdStation = CmdStation
            };
            dpm.To(OutputPrinter);
            try
            {
                dpm.SetDeviceOwner(_cn);
                return true;
            }
            catch (Exception e)
            {
                Logger.Warn("", e);
                return SetDpmWithDpmAndPutSettings();
            }
        }
        protected virtual bool SetDpmWithDpmAndPutSettings()
        {
            WriteLine("using dpm step 2");
            var dpm = new DevicePolicyManager(DeviceSelectedOnCreating)
            {
                CmdStation = CmdStation
            };
            dpm.To(OutputPrinter);
            try
            {
                using (CommandExecutor executor = new CommandExecutor())
                {
                    executor.To(OutputPrinter);
                    executor.AdbShell(DeviceSelectedOnCreating, "settings put global device_provisioned 0");
                    dpm.SetDeviceOwner(_cn);
                    executor.AdbShell(DeviceSelectedOnCreating, "settings put global device_provisioned 1");
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Warn("", e);
                return false;
            }
        }

        protected virtual bool SetDpmWithDpmPro(CstmDpmCommander dpmPro)
        {
            try
            {
                WriteLine("using dpm pro");
                dpmCommander.SetDeviceOwner(_cn);
                return true;
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
                return false;
            }
        }

        protected override string GetTipByExitCode(int exitCode)
        {
            Ux.Message(Res("EDpmSetterBaseFinishedMessage"));
            WriteLine(Res("EDpmSetterBaseFinishedMessage"));
            return Res("EDpmSetterBaseFinished");
            //switch (Args.CurrentThread.ExitCode)
            //{
            //    case CstmDpmCommander.OKAY:
            //        WriteLine(Res("EDpmSetterBaseMsgSuccessed"));
            //        return Res("EDpmSetterBaseTipSuccessed");
            //    case CstmDpmCommander.ERR:
            //        WriteLine(Res("EDpmSetterBaseMsgError"));
            //        return Res("EDpmSetterBaseTipError");
            //    case CstmDpmCommander.ERR_EXIST_OTHER_ACC:
            //        WriteLine(Res("EDpmSetterBaseMsgErrOtherAccounts"));
            //        return Res("EDpmSetterBaseTipErrOtherAccounts");
            //    case CstmDpmCommander.ERR_EXIST_OTHER_USER:
            //        WriteLine(Res("EDpmSetterBaseMsgErrOtherUsers"));
            //        return Res("EDpmSetterBaseTipErrOtherUsers");
            //    case CstmDpmCommander.ERR_MIUI_SEC:
            //        WriteLine(Res("EDpmSetterBaseMsgErrMIUISec"));
            //        return Res("EDpmSetterBaseTipErrMiuiSec");
            //    default:
            //        return base.GetTipByExitCode(exitCode);
            //}
        }
    }
}
