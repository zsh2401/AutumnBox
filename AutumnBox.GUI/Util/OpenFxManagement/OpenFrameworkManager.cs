/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Fast;
using AutumnBox.OpenFramework.Management;
using System.Linq;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal static class OpenFrameworkManager
    {
        private const string TAG = "OFM";
        public static void Init()
        {
            SLogger.Info(TAG, "OpenFx loading");
            SLogger.Info(TAG, "Init OpenFx env");
            OpenFxLoader.InitEnv(GUIApiManager.BaseApiInstance);
            SLogger.Info(TAG, "OpenFx env inited");
            SLogger.Info(TAG, "Load extensions");
            OpenFxLoader.LoadExtensions();
            SLogger.Info(TAG,$"There are {OpenFxLoader.LibsManager.Librarians.Count()} librarians and {OpenFxLoader.LibsManager.Wrappers().Count()} wrappers");
            SLogger.Info(TAG, "Loaded extensions");
        }
    }
}
