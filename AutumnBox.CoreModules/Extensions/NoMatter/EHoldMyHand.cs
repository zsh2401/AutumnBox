/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;

namespace AutumnBox.CoreModules.Extensions.Hidden
{
    [ExtName("Example extension")]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtDeveloperMode]
    [ExtText("fuck","Hello","zh-cn:你好!")]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LProperty]
        private IUx Ux { get; set; }

        [LProperty]
        private IDevice Device { get; set; }

        [LMain]
        public void Main(IDevice device, Context context, ILeafUI ui, IUx ux, Dictionary<string, object> data,TextAttrManager manager)
        {
            using (ui)
            {
                ui.Show();
                ui.WriteLine(manager["fuck"]);
                ui.WriteOutput("fuck asdasjkdshadskjhkj");
                ui.ShowMessage("WTF\n\n\n\n\n\nasdadas\n\n\nasdasdsahsdkajghsdakjfhsdjkaghsdfjkghjkfsdhgjkshfdjkgfhsjdgkhdskfjghjW");
                bool? choice = ui.DoChoice("FUCcqwjeiwqeqehqWK");
                ui.WriteLine(choice.ToString());
                bool yn = ui.DoYN("FUCK2c   2rqwhewqhehqweqhwewqhejwqhewqjhqwejkqwhewqhWW");
                ui.WriteLine(yn.ToString());
                object result = ui.SelectFrom(new object[] { "a","qqdasdsadasb","c","a","s","c","a"},"选择你要做的SB操作");
                ui.WriteLine(result);
                ui.Finish();
            }
        }
    }
}
