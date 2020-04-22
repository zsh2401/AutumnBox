/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/30 22:46:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic;
using AutumnBox.GUI.Properties;
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
                BasicBooter.Free();
                //TaskKill.Kill("adb.exe");
            }
            catch { }
        }
    }
}
