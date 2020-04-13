using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Extension.Leaf;
using AutumnBox.OpenFramework.Extension.Leaf.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Essentials.Extensions
{
    [ExtHidden]
    class EAutumnBoxGuideViewer : LeafExtensionBase
    {
        private const string URL_GUIDE = "https://www.atmb.top/guide";

        [LMain]
        public void EntryPoint(string path = null)
        {
            Process.Start(Path.Combine(URL_GUIDE, path));
        }
    }
}
