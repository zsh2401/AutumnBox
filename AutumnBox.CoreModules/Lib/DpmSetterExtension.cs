/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 17:36:13 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Lib
{
    [ExtDesc("使用奇淫技巧暴力设置设备管理员,\n注意:使用此模块前,必须先移除屏幕锁,指纹锁等,否则将可能导致不可预见的后果")]
    [ExtDesc("Use the sneaky skills to set up the device administrator, \n Note: Before using this module, you must first remove the screen lock, fingerprint lock, etc., otherwise it may lead to unforeseen consequences", Lang = "en-us")]
    internal abstract class DpmSetterExtension : OfficialVisualExtension
    {
        public abstract string ReceiverClassName { get; }
        public abstract string DpmAppPackageName { get; }
        protected GodPower GodPower { get; set; }
        protected virtual bool OnWarnUser()
        {
            string warnMsg = string.Format(Res("EGodPowerWarningFmt"), Res("AppNameThis"));
            return Ux.Agree(warnMsg);
        }

        protected virtual int SetReciverAsDpm()
        {
            WriteLineAndSetTip(Res("DPMSetting"));
            GetDevcieShellCommand
                ($"dpm set-device-owner {DpmAppPackageName}/{ReceiverClassName}")
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();
            return 0;
        }

        protected sealed override int VisualMain()
        {
            WriteWaitingForUser();
            if (!OnWarnUser())
            {
                return ERR_CANCELED_BY_USER;
            }
            ProcessBasedCommand command = null;
            IProcessBasedCommandResult result = null;
            WriteInitInfo();
            GodPower = new GodPower(this, TargetDevice);

            WriteLineAndSetTip(Res("EGodPowerExtractingApk"));
            GodPower.Extract();
            ThrowIfCanceled();

            WriteLineAndSetTip(Res("EGodPowerPushingApk"));
            command = GodPower.GetPushCommand();
            CmdStation.Register(command);
            result = command
                .To(OutputPrinter)
                .Execute();

            WriteLineAndSetTip(Res("EGodPowerRmUser"));
            command = GodPower.GetRemoveUserCommand();
            CmdStation.Register(command);
            result = command
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();

            WriteLineAndSetTip(Res("EGodPowerRmAcc"));
            command = GodPower.GetRemoveAccountCommnad();
            CmdStation.Register(command);
            result = command
                .To(OutputPrinter)
                .Execute();
            ThrowIfCanceled();

            return SetReciverAsDpm();
        }
    }
}
