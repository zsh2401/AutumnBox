using AutumnBox.Leafx.Enhancement.ClassTextKit;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Open;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    [ClassText("confirm", "Are you sure?", "zh-cn:确定要重启吗?")]
    class EAutumnBoxRestarter : LeafExtensionBase
    {
        [LMain]
        public void EntryPoint(IAppManager app, IUx ux, ClassTextReader reader)
        {
            if (ux.DoYN(reader["confirm"]))
            {
                app.RestartAppAsAdmin();
            }
        }
    }
}
