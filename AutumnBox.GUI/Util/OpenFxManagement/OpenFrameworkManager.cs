/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal static class OpenFrameworkManager
    {
        public static void Init()
        {
            SLogger.Info("OFM", "Open Fx loading");
            FxLoader.LoadBase(new AutumnBox_GUI_Calller());
            FxLoader.LoadExtensions();
            SLogger.Info("OFM", "Open Fx loaded ExtensionManager");
        }
    }
}
