/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:48:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.CoreModules.Lib;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT激活冰箱")]
    [ExtAppProperty("com.catchingnow.icebox")]
    [ExtIcon("Icons.icebox.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    public class EGodPower_IceBox : BasedOnGodPowerExtension
    {
        public override string ReceiverClassName => null;

        public override string DpmAppPackageName => null;

        protected override bool OnWarnUser()
        {
            var warnMsg = string.Format(Res("DPMWarningFmt"), Res("AppNameIceBox"));
            return Ux.Agree(warnMsg);
        }
        protected override int SetReciverAsDpm()
        {
            return GodPower
                 .GetSetIceBoxCommand()
                 .To(OutputPrinter)
                 .Execute()
                 .ExitCode;
        }
    }
}
