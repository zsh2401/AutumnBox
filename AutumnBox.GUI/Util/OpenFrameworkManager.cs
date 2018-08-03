/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.OpenApiImpl;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.Util
{
    internal static class OpenFrameworkManager
    {
        public static void Init()
        {
            Logger.Info("OFM", "Init ExtensionManager");
            Manager.InitFramework(new AppManagerImpl());
            Logger.Info("OFM", "Inited ExtensionManager");
        }
    }
}
