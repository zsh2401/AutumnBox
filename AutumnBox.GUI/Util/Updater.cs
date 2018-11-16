/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 18:53:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.View.Windows;

namespace AutumnBox.GUI.Util
{
    public static class Updater
    {
        static Updater()
        {
            if (Settings.Default.SkipVersion == "NULL")
            {
                Settings.Default.SkipVersion = Self.Version.ToString();
                Settings.Default.Save();
            }
        }
        public static void CheckAndNotice()
        {
            RemoteVersionInfoGetter getter = new RemoteVersionInfoGetter();
            getter.Try((e) =>
            {
                if (e.Version > Self.Version && e.VersionString != Settings.Default.SkipVersion)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        new UpdateNoticeWindow(e).Show();
                    });
                }
            });
        }
    }
}
