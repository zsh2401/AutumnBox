using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.AppFx;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension.Fast;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.OpenFramework.Fast
{
    [ExtText("msg", "Do you have install the relative app?", "zh-cn:你似乎没有安装对应APP?")]
    [ExtText("continue", "Continue forcely", "zh-cn:强行继续")]
    [ExtText("ok", "Ok", "zh-cn:好")]
    [ExtText("cancel", "Cancel", "zh-cn:取消")]
    static class LeafExtensionExt
    {
        static readonly TextAttrManager text = new TextAttrManager(typeof(LeafExtensionExt));
        public static void AppPropertyCheck(ILeafUI ui, IDevice device, string packageName)
        {
#pragma warning disable CS0618 // 类型或成员已过时
            bool isInstall = new PackageManager(device).IsInstall(packageName) == true;
#pragma warning restore CS0618 // 类型或成员已过时
            if (!isInstall)
            {
                bool? choice = ui.DoChoice(text["msg"], text["ok"], text["continue"], text["cancel"]);
                if (choice == null || choice == true) ui.EShutdown();
            }
        }
    }
}
