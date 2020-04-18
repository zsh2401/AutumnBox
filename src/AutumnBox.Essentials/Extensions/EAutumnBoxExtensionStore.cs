using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using System.Diagnostics;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxExtensionStore
    {
        public const string URL_EXTENSION_STORE = "https://atmb.top/extensions/";

        [LMain]
        public void EntryPoint()
        {
            Process.Start(URL_EXTENSION_STORE);
        }
    }
}
