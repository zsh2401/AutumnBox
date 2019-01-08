using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("激活第二空间伪装版")]
    [ExtIcon("Icons.anzenbokusufake.png")]
    [ExtRegions("zh-CN")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [ExtAppProperty(PKG_NAME)]
    [DpmReceiver(RECEIVER_NAME)]
    class EHideAnzen : EDpmSetterBase
    {
        public const string PKG_NAME = "com.hld.anzenbokusufake";
        public const string RECEIVER_NAME = "com.hld.anzenbokusufake/com.hld.anzenbokusu.receiver.DPMReceiver";
    }
}
