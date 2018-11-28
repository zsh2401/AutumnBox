using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Restart AutumnBox","zh-CN:重启秋之盒")]
    [ExtAuth("zsh2401","zh-cn:秋之盒官方")]
    [ExtMinApi(8)]
    [ExtTargetApi(8)]
    [ContextPermission(CtxPer.High)]
    [ExtIcon("Icons.restart.png")]
    internal class ERestartApp : AutumnBoxExtension
    {
        public override int Main(Dictionary<string, object> data)
        {
            string msg = CoreLib.Current.Languages.Get("ERestartAppMsg");
            string btnAdmin = CoreLib.Current.Languages.Get("ERestartAppBtnAdmin");
            string btnNormal = CoreLib.Current.Languages.Get("ERestartAppBtnNormal");
            ChoiceResult choiceResult =  Ux.DoChoice(msg, btnAdmin, btnNormal);
            switch (choiceResult)
            {
                case ChoiceResult.Left:
                    App.RestartAppAsAdmin();
                    break;
                case ChoiceResult.Right:
                    App.RestartApp();
                    break;
                default:
                    return ERR_CANCELED_BY_USER;
            }
            return OK;
        }
    }
}
