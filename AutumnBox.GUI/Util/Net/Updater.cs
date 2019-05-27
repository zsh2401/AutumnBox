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
using System;
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
            MainWindowBus.Info("正在检测更新");
            getter.Advance().ContinueWith(result =>
            {
                if (Result.Version > Self.Version)
                {
                    MainWindowBus.Info("检测到更新");
                }
            });
        }
    }
}
