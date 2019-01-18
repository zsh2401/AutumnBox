/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 19:19:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2;
using AutumnBox.Basic.ManagedAdb;
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
        public void Main(IDevice device, Context context, ILeafUI ui, IUx ux, Dictionary<string, object> data)
        {
            using (ui)
            {
                ui.Show();
                using (var executor = new CommandExecutor())
                {
                    executor.To((e) => ui.WriteLine(e));
                    using (AndroidShell shell = new AndroidShell(executor, device))
                    {
                        shell.WriteLine("ls");
                        shell.WriteLine("ping www.baidu.com");
                        Thread.Sleep(5000);
                    }
                }
                Thread.Sleep(2000);
                ui.Shutdown();
                ui.WriteLine("wtf");
            }
        }
    }
}
