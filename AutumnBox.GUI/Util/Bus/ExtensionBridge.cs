using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Bus
{
    static class ExtensionBridge
    {
        public static void Start(string className)
        {
            AtmbContext.Instance.NewExtensionThread(className)?.Start();
        }
    }
}
