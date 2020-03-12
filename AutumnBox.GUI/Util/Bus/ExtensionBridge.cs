using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.GUI.Util.Bus
{
    static class ExtensionBridge
    {
        public static void Start(string className)
        {
            try
            {
                LakeProvider.Lake.Get<ITaskManager>().CreateNewTaskOf(className)?.Start();
            }
            catch (Exception e)
            {
                SLogger.Warn(nameof(ExtensionBridge), "extension not found", e);
            }
        }
    }
}
