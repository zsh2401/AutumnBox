/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]

    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    //[UserAgree("Please be true")]
    [ExtDeveloperMode]
    //[ExtRequireRoot]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LProperty]
        private IUx Ux { get; set; }

        [LProperty]
        private IDevice Device { get; set; }

        //[LSignalReceive(Signals.ON_CREATED)]
        //private void OnCreate(string message, ExtensionArgs args)
        //{
        //    Ux.Message($"message->{message} " + args.Wrapper.ToString() ?? "NULL");
        //}
        public void Main(ILeafUI ui, IUx ux, Dictionary<string, object> data)
        {
            using (ui)
            {
                ui.Show();
                ui.WriteLine("Hello Leaf UI!");
                Thread.Sleep(2000);
                ui.Finish();
            }
        }
    }
}
