using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Mix
{
    [ExtName("Restart AutumnBox","zh-CN:重启秋之盒")]
    [ExtAuth("zsh2401","zh-cn:秋之盒官方")]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ContextPermission(CtxPer.High)]
    [ExtIcon("Icons.restart.png")]
    internal class ERestartApp : LeafExtensionBase
    {
        [LMain]
        public int Main(IUx ux,IAppManager app)
        {
            string msg = CoreLib.Current.Languages.Get("ERestartAppMsg");
            string btnAdmin = CoreLib.Current.Languages.Get("ERestartAppBtnAdmin");
            string btnNormal = CoreLib.Current.Languages.Get("ERestartAppBtnNormal");
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
