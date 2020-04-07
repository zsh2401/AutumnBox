using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Open.LKit;

namespace AutumnBox.Essentials
{
    [ExtName("Hello world extension")]
    class EHelloWorld : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(ILeafUI ui)
        {
            using (ui)
            {
                ui.Title = this.GetName();
                ui.Icon = this.GetIconBytes();
                ui.Show();
                ui.WriteLine("Hello world!");
                ui.Finish();
            }
        }
    }
}
