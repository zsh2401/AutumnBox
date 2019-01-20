/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/19 20:54:58 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活第二空间","en-us:Set Anzenboksu as DPM")]
    [ExtIcon("Icons.Anzenbokusu.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtAppProperty(PKG_NAME)]
    [DpmReceiver(RECEIVER_NAME)]
    internal class EAnzenboksuActivator : EDpmSetterBase
    {
        public const string PKG_NAME = "com.hld.anzenbokusu";
        public const string RECEIVER_NAME = "com.hld.anzenbokusu/.receiver.DPMReceiver";
    }
}
