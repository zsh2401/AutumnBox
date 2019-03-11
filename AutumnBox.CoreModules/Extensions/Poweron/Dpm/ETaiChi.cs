using AutumnBox.OpenFramework.Extension;

namespace AutumnBox.CoreModules.Extensions.Poweron.Dpm
{
    [ExtName("Act TaiChi", "zh-cn:一键激活太极阴阳门")]
    [ExtIcon("Icons.taichi.png")]
    class ETaiChi : DeviceOwnerSetter
    {
        protected override string ComponentName => "me.weishu.exp";

        protected override string PackageName => "me.weishu.exp/.DeviceAdmin";
    }
}
