/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 22:46:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Services.Impl.OS;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.GUI.Util
{
    public class AppUnloader
    {
        public static AppUnloader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AppUnloader();
                }
                return _instance;
            }
        }
        private static AppUnloader _instance;
        private AppUnloader() { }
        public void Unload()
        {
            try
            {
                if (Settings.Default.IsFirstLaunch && Settings.Default.GuidePassed)
                {
                    Settings.Default.IsFirstLaunch = false;
                }
                Settings.Default.Save();
                try { OpenFx.Unload(); } catch { }
                if (Adb.Server?.IsEnable == true)
                {
                    var server = Adb.Server;
                    Adb.Load(null);
                    server.Kill();
                    TaskKill.Kill("adb.exe");
                }
            }
            catch { }
        }
    }
}
