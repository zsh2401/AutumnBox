using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("激活第二空间伪装版")]
    [ExtIcon("Icons.anzenbokusufake.png")]
    [ExtRegions("zh-CN")]
    class EHideAnzen : DeviceOwnerSetter
    {
        protected override string PackageName => "com.hld.anzenbokusufake";
        protected override string ComponentName => "com.hld.anzenbokusufake/com.hld.anzenbokusu.receiver.DPMReceiver";
    }
}
