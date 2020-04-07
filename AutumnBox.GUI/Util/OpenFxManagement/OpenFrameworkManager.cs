/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System.Linq;
using AutumnBox.OpenFramework.Leafx.Container;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal static class OpenFrameworkManager
    {
        private const string TAG = "OFM";
        public static void Init()
        {
            var api = new AutumnBoxGuiBaseApiImpl();
            SLogger.Info(TAG, "Init OpenFx env");
            OpenFx.Load(api);
            SLogger.Info(TAG, "OpenFx env inited");
            SLogger.Info(TAG, "Load extensions");
            OpenFx.RefreshExtensionsList();
            ILibsManager libsManager = OpenFx.Lake.Get<ILibsManager>();
            SLogger.Info(TAG, $"There are {libsManager.Librarians.Count()} librarians and {libsManager.Wrappers().Count()} wrappers");
            SLogger.Info(TAG, "Loaded extensions");
        }
    }
}
