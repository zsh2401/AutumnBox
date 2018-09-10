/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 1:30:57 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Adb;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Extension;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot
{
    [ExtName("免ROOT激活小黑屋")]
    [ExtAppProperty("com.web1n.stopapp")]
    [ExtIcon("Icons.stopapp.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EGodPower_StopApp : BasedOnGodPowerExtension
    {
        public override string ReceiverClassName => throw new NotImplementedException();

        public override string DpmAppPackageName => throw new NotImplementedException();

        protected override bool OnWarnUser()
        {
            var warnMsg = string.Format(Res("DPMWarningFmt"), Res("AppNameStopApp"));
            return Ux.Agree(warnMsg);
        }
        protected override int SetReciverAsDpm()
        {
            return GodPower
                .GetSetStopAppCommand()
                .To(OutputPrinter)
                .Execute().ExitCode;
        }
    }
}
