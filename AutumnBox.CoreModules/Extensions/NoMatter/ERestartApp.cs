using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.CoreModules.Extensions.NoMatter
{
    [ExtName("Restart AutumnBox","zh-CN:重启秋之盒")]
    [ExtAuth("zsh2401","zh-cn:秋之盒官方")]
    [ExtHide]
    [ContextPermission(CtxPer.High)]
    [ExtIcon("Icons.restart.png")]
    [ExtRequiredDeviceStates(LeafConstants.NoMatter)]
    [ExtText("msg","Reboot to which mode?","zh-cn:重启到哪一个模式?")]
    [ExtText("user", "User", "zh-cn:用户")]
    [ExtText("admin", "Administrator", "zh-cn:管理员")]
    internal class ERestartApp : LeafExtensionBase
    {
        [LMain]
        public int Main(IUx ux, IAppManager app, IClassTextDictionary text)
        {
            string msg = text["msg"];
            string btnAdmin = text["admin"];
            string btnNormal = text["user"];
            ChoiceResult choiceResult = ux.DoChoice(msg, btnAdmin, btnNormal);
            switch (choiceResult)
            {
                case ChoiceResult.Left:
                    app.RestartAppAsAdmin();
                    break;
                case ChoiceResult.Right:
                    app.RestartApp();
                    break;
                default:
                    return AutumnBoxExtension.ERR_CANCELED_BY_USER;
            }
            return AutumnBoxExtension.OK;
        }
    }
}
