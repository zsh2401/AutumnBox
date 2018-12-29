/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 18:53:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.View.Windows;
using System;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    internal static class Updater
    {
        public static RemoteVersionInfoGetter.Result Result { get; private set; }
        static Updater()
        {
            if (Settings.Default.SkipVersion == "NULL")
            {
                Settings.Default.SkipVersion = Self.Version.ToString();
                Settings.Default.Save();
            }
        }
        public static Task RefreshAsync(Action callback)
        {
            return Task.Run(() =>
            {
                RemoteVersionInfoGetter getter = new RemoteVersionInfoGetter();
                try
                {
                    Result = getter.GetSync();
                    callback?.Invoke();
                }
                catch (Exception e)
                {
                    SLogger.Warn(nameof(Updater), "cannot refresh update informations", e);
                }
            });
        }
        public static void ShowUI(bool showIsLatestVersion = true, bool showSkippedVersion = false)
        {
            if (Result == null) return;

            if (Result.Version > Self.Version &&
                (Result.Version > Version.Parse(Settings.Default.SkipVersion) ||
                showSkippedVersion))
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    new UpdateNoticeWindow().Show();
                });
            }
            else if (showIsLatestVersion)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    new MessageWindow()
                    {
                        MsgTitle = "PanelSettingsTitleDontNeedUpdate",
                        Message = "PanelSettingsMsgDontNeedUpdate",
                        Owner = App.Current.MainWindow
                    }.Show();
                });
            }
        }
    }
}
