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
using System.Threading;

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
        public void Main(IDevice device,Context context, ILeafUI ui, IUx ux, Dictionary<string, object> data)
        {
            using (ui)
            {
                ui.Icon = this.GetIconBytes();
                ui.CloseButtonClicked += (s, e) =>
                {
                    e.CanBeClosed = false;
                };
                ui.Title = "Mother fucker";
                ui.Show();

                var thread = context.NewExtensionThread("ELeafUIDemo");
                //thread.Data["test"] = "b";
                thread.Start();

                ui.WriteLine(device?.ToString() ?? "null");
                ui.WriteLine("Hello Leaf UI!");
                Thread.Sleep(2000);
                ui.Finish();
            }
        }
    }
}
