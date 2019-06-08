using AutumnBox.Logging;
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
            try
            {
                AtmbContext.Instance.NewExtensionThread(className)?.Start();
            }
            catch (Exception e)
            {
                SLogger.Warn(nameof(ExtensionBridge), "extension not found", e);
            }
        }
    }
}
