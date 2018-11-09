/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/28 23:48:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Extension;
using AutumnBox.CoreModules.Lib;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.CoreModules.Attribute;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("免ROOT激活冰箱", "en-us:Set Icebox as DPM without root")]
    [ExtAppProperty(PKG_NAME)]
    [ExtIcon("Icons.icebox.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [DpmReceiver("自行覆写SetDpm方法，用不上这个，任性！")]
    internal class EIceBox : EDpmSetterBase
    {
        private const string PKG_NAME = "com.catchingnow.icebox";

        protected override int SetDpm()
        {
            return CmdStation.Register(GodPower
                 .GetSetIceBoxCommand())
                 .To(OutputPrinter)
                 .Execute()
                 .ExitCode;
        }
    }
}
