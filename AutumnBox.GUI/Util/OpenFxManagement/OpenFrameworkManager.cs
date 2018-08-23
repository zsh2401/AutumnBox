/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Management;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal static class OpenFrameworkManager
    {
        public static void Init()
        {
            Logger.Info("OFM", "Init ExtensionManager");
            Manager.InitFramework(new AutumnBox_GUI_Calller());
            Logger.Info("OFM", "Inited ExtensionManager");
        }
    }
}
