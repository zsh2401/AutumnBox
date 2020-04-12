using AutumnBox.Logging;
using AutumnBox.OpenFramework.Open;
using System;
using AutumnBox.Leafx.Container;

namespace AutumnBox.GUI.Util.Bus
{
    [Obsolete("Use service to instead",true)]
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
                SLogger.Warn(nameof(ExtensionBridge), $"Can not run extension : {className}", e);
            }
        }
    }
}
