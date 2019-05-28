/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/10 18:53:28 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.Net;
using AutumnBox.GUI.Util.Net.Getters;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Logging;
using HandyControl.Controls;
using HandyControl.Data;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net
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
        public static void Do()
        {
            var getter = new RemoteVersionInfoGetter();
            MainWindowBus.Info("Update.CheckingUpdate");
            getter.Advance().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    MainWindowBus.Error("Update.Failed");
                }
                else
                {
                    if (task.Result.Version > Self.Version)
                    {
                        App.Current.Dispatcher.Invoke(() =>
                        {
                            DoAsk(task.Result);
                        });
                    }
                    else
                    {
                        MainWindowBus.Success("Update.IsLatestVersion");
                    }
                }
            });
        }
        public static void DoAsk(RemoteVersionInfoGetter.Result result)
        {
            GrowlInfo gInfo = new GrowlInfo
            {
                ConfirmStr = App.Current.Resources["Update.UpdateNow"].ToString(),
                CancelStr = App.Current.Resources["Update.Cancel"].ToString(),
                Message = $"{App.Current.Resources["Update.HaveAUpdate"]}  v{result.Version}\n{result.Message}",
                Token = MainWindowBus.TOKEN_PANEL_MAIN,
                ActionBeforeClose = (isConfirmed) =>
                {
                    if (isConfirmed)
                    {
                        try { Process.Start(result.UpdateUrl); } catch { }
                    }
                    return true;
                }
            };
            Growl.Ask(gInfo);
        }
    }
}
