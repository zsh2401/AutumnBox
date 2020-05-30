using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using System.Diagnostics;
using System.IO;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxGuideViewer : LeafExtensionBase
    {
        private const string URL_GUIDE = "https://www.atmb.top/guide";

        [LMain]
        public void EntryPoint(string path = "")
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = Path.Combine(URL_GUIDE, path),
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}
