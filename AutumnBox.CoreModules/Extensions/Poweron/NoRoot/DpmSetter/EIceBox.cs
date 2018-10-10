/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:48:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.CoreModules.Lib;
using AutumnBox.Basic.Device.Management.AppFx;

namespace AutumnBox.CoreModules.Extensions.Poweron.NoRoot.DpmSetter
{
    [ExtName("免ROOT激活冰箱")]
    [ExtName("Set Icebox as DPM without root", Lang = "en-us")]
    [ExtAppProperty("com.catchingnow.icebox")]
    [ExtIcon("Icons.icebox.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EIceBox : DpmSetterExtension
    {
        protected override ComponentName ReceiverName => throw new System.NotImplementedException();

        protected override int SetReciverAsDpm()
        {
            return CmdStation.Register(GodPower
                 .GetSetIceBoxCommand())
                 .To(OutputPrinter)
                 .Execute()
                 .ExitCode;
        }
    }
}
