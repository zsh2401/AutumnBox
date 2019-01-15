using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using AutumnBox.OpenFramework.Open;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.CoreModules.Extensions
{
    [ExtName("Leaf UI Demo")]
    [ExtRequiredDeviceStates(AutumnBoxExtension.NoMatter)]
    [ExtDeveloperMode]
    [ExtIcon("Icons.Usersir.png")]
    public class ELeafUIDemo : LeafExtensionBase
    {
        [LMain]
        public void Main([LFromData]string test, ILeafUI ui)
        {
            using (ui)
            {
                ui.Show();
                ui.WriteLine(test ?? "null");
                ui.Finish();
            }
        }
    }
}
