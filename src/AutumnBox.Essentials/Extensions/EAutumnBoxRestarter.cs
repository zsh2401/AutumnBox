using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    [ExtText("confirm", "Are you sure?", "zh-cn:确定要重启吗?")]
    class EAutumnBoxRestarter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IAppManager app, IUx ux, IClassTextReader reader)
        {
            var texts = reader.Read(this);
            if (ux.DoYN(texts["confirm"]))
            {
                app.RestartAppAsAdmin();
            }
        }
    }
}
