using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using System.Diagnostics;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxExtensionStore : LeafExtensionBase
    {
        public const string URL_EXTENSION_STORE = "https://atmb.top/extensions/";

        [LMain]
        public void EntryPoint()
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = URL_EXTENSION_STORE,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
