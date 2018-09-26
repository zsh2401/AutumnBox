/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 1:30:57 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT激活小黑屋")]
    [ExtName("Set StopApp as DPM without root", Lang = "en-us")]
    [ExtAppProperty("com.web1n.stopapp")]
    [ExtIcon("Icons.stopapp.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EStopApp : DpmSetterExtension
    {
        public override string ReceiverClassName => throw new NotImplementedException();

        public override string DpmAppPackageName => throw new NotImplementedException();

        protected override bool OnWarnUser()
        {
            var warnMsg = string.Format(Res("EGodPowerWarningFmt"), Res("AppNameStopApp"));
            return Ux.Agree(warnMsg);
        }
        protected override int SetReciverAsDpm()
        {
            return CmdStation.Register(
                GodPower
                .GetSetStopAppCommand()
                )
                .To(OutputPrinter)
                .Execute().ExitCode;
        }
    }
}
