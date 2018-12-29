using AutumnBox.CoreModules.Attribute;
using AutumnBox.CoreModules.Lib;
using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtName("Activate Island by on key", "zh-cn:一键激活岛")]
    [ExtAppProperty(PKG_NAME)]
    [ExtIcon("Icons.Island.png")]
    [ExtRequiredDeviceStates(Basic.Device.DeviceState.Poweron)]
    [DpmReceiver(RECEIVER_NAME)]
    class EIslandActivator : EDpmSetterBase
    {
        private const string PKG_NAME = "com.oasisfeng.island";
        private const string RECEIVER_NAME = "com.oasisfeng.island/.IslandDeviceAdminReceiver";
    }
}
