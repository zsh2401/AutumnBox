/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 1:30:57 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活小黑屋", "en-us:Set StopApp as DPM without root")]
    [ExtAppProperty(PKG_NAME)]
    [ExtIcon("Icons.stopapp.png")]
    [ExtPriority(ExtPriority.HIGH + 1)]
    [DpmReceiver(COMPONENT_NAME)]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    internal class EStopApp : EDpmSetterBase
    {
        private const string PKG_NAME = "web1n.stopapp";
        private const string COMPONENT_NAME = "web1n.stopapp/.receiver.AdminReceiver";
    }
}
