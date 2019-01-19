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
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LProperty]
        private IUx Ux { get; set; }

        [LProperty]
        private IDevice Device { get; set; }

        [LMain]
        public void Main(IDevice device, Context context, ILeafUI ui, IUx ux, Dictionary<string, object> data)
        {
            using (ui)
            {
                ui.Show();
                ui.WriteOutput("fuck asdasjkdshadskjhkj");
                ui.ShowMessage("WTF");
                bool? choice = ui.DoChoice("FUCK");
                ui.WriteLine(choice.ToString());
                bool yn = ui.DoYN("FUCK2");
                ui.WriteLine(yn.ToString());
                object result = ui.SelectFrom(new object[] { "a","b"});
                ui.WriteLine(result);
                ui.Finish();
            }
        }
    }
}
