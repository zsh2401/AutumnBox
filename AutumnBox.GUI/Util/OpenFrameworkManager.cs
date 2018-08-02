/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.OpenApiImpl;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.Util
{
    internal static class OpenFrameworkManager
    {
        public static void Init()
        {
            Logger.Debug("OFM", "Injecting Open framework api");
            AutumnBoxGuiApiProvider.Inject(new AppManagerImpl());
            Logger.Debug("OFM", "Injected Open framework api");
            Logger.Debug("OFM", "Init ExtensionManager");
            var instace = ExtensionManager.Instance;
            Logger.Debug("OFM", "Inited ExtensionManager");
        }
    }
}
