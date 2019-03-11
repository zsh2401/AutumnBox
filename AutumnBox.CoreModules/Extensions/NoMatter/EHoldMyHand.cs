/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Attributes;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Example extension")]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(LeafConstants.NoMatter)]
    [ExtDeveloperMode]
    [ExtText("fuck", "Hello", "zh-cn:你好!")]
    //[ExtMinAndroidVersion(9, 0, 0)]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LProperty]
        private IUx Ux { get; set; }

        [LProperty]
        private IDevice Device { get; set; }

        [LMain]
        public void Main(IDevice device, IAppManager app, ILogger<string> logger, Context context, ILeafUI ui, IUx ux, Dictionary<string, object> data, TextAttrManager manager)
        {
            using (ui)
            {
                ui.Show();
                logger.Debug("WTF");
                using (CommandExecutor executor = new CommandExecutor())
                {
                    ui.CloseButtonClicked += (s, e) =>
                    {
                        e.CanBeClosed = true;
                        executor.Dispose();
                    };
                    executor.To(e => ui.WriteOutput(e.Text));
                    executor.Adb("help");
                }
                CoreLib.Current.TEST = false;
                app.RefreshExtensionView();
                ui.ShowMessage("meile!");
                CoreLib.Current.TEST = true;
                app.RefreshExtensionView();
                ui.WriteLine(manager["fuck"]);
                ui.WriteOutput("fuck asdasjkdshadskjhkj");
                ui.ShowMessage("WTF\n\n\n\n\n\nasdadas\n\n\nasdasdsahsdkajghsdakjfhsdjkaghsdfjkghjkfsdhgjkshfdjkgfhsjdgkhdskfjghjW");
                bool? choice = ui.DoChoice("FUCcqwjeiwqeqehqWK");
                ui.WriteLine(choice.ToString());
                bool yn = ui.DoYN("FUCK2c   2rqwhewqhehqweqhwewqhejwqhewqjhqwejkqwhewqhWW");
                ui.WriteLine(yn.ToString());
                //object result = ui.SelectFrom(new object[] { "a", "qqdasdsadasb", "c", "a", "s", "c", "a" }, "选择你要做的SB操作");
                //ui.WriteLine(result);
                ui.Finish();
            }
        }
    }
}
