using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("Set Island as DPM", "zh-cn:一键激活岛")]
    [ExtIcon("Icons.Island.png")]
    class EIslandActivator : DeviceOwnerSetter
    {
        protected override string ComponentName => "com.oasisfeng.island/.IslandDeviceAdminReceiver";
        protected override string PackageName => "com.oasisfeng.island";
    }
}
