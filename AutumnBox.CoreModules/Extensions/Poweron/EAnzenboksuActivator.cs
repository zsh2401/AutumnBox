using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活第二空间")]
    [ExtIcon("Icons.Anzenbokusu.png")]
    [ExtRegions("zh-CN")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtAppProperty(PKG_NAME)]
    [DpmReceiver(RECEIVER_NAME)]
    internal class EAnzenboksuActivator : EDpmSetterBase
    {
        public const string PKG_NAME = "com.hl.danzenbokusu";
        public const string RECEIVER_NAME = "com.hl.danzenbokusu/.receiver.DPMReceiver";
    }
}
