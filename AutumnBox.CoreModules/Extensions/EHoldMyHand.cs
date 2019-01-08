/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.CoreModules.Aspect;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Running;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Example extension")]
    //[ExtDeveloperMode(true)]
    [ExtIcon("Icons.flash.png")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    //[UserAgree("Please be true")]
    internal class EHoldMyHand : LeafExtensionBase
    {
        [LProperty]
        private IUx Ux { get; set; }

        [LSignalReceive(Signals.ON_CREATED)]
        private void OnCreate(string message, ExtensionArgs args)
        {
            Ux.Message($"message->{message} " + args.Wrapper.ToString() ?? "NULL");
        }
        public void Main(IUx ux,Dictionary<string,object> data)
        {
            ux.Message("Leaf extension!");
            ux.Message(data.Count().ToString());
        }
    }
}
