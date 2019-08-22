using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.LeafExtension;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions.Poweron
{
    [ExtDeveloperMode]
    class EFluidPermissionGranter : LeafExtensionBase
    {
        public void Main(ILeafUI ui,ICommandExecutor executor,IDevice device) {
            using (ui) {
                ui.Title = "流体手势激活器";
                ui.Show();
                executor.OutputReceived += (s, e) => ui.WriteOutput(e.Text);
                executor.AdbShell(device,"");
                ui.Finish();
            }
        }
    }
}
