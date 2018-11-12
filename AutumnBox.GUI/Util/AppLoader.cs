/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/19 20:39:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.ManagedAdb;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.OpenFxManagement;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    class AppLoader
    {
        #region OTHER
        public interface ILoadingUI
        {
            string LoadingTip { set; }
            double Progress { set; }
            void Finish();
        }
        private readonly ILoadingUI ui;
        public AppLoader(ILoadingUI ui) : this()
        {
            this.ui = ui;
        }
        public void LoadAsync(Action callback)
        {
            Task.Run(() =>
            {
                Load();
                callback?.Invoke();
            });
        }
        #endregion

        private readonly ILogger logger;
        public AppLoader()
        {
            logger = new Logger<AppLoader>();
        }
        #region LOADING_FLOW
        private void Load()
        {
            LoggingStation.Instance.Work();
            ui.Progress = 0;
            //如果设置在启动时打开调试窗口
            if (Settings.Default.ShowDebuggingWindowNextLaunch)
            {
                //打开调试窗口
                App.Current.Dispatcher.Invoke(() =>
                {
                    new LogWindow().Show();
                });
            }
            logger.Info("");
            logger.Info("======================");
            logger.Info($"Run as " + (Self.HaveAdminPermission ? "Admin" : "Normal user"));
            logger.Info($"AutumnBox version: {Self.Version}");
            logger.Info($"SDK version: {BuildInfo.SDK_VERSION}");
            logger.Info($"Windows version {Environment.OSVersion.Version}");
            logger.Info("======================");
            Basic.Util.Debugging.LoggingStation.Logging += (s, e) =>
            {

#if !DEBUG
                if (e.Tag.ToLower() == "debug") return;
#endif
                LoggingStation.Instance.Log(e.Tag, e.Level.ToString(), e.Text);
            };
            ui.Progress = 30;
            ui.LoadingTip = App.Current.Resources["ldmsgStartAdb"].ToString();

            try
            {
                logger.Info("trying starts adb server ");
                Adb.DefaultLoad();
                logger.Info("adb server starts successed");
            }
            catch (AdbCommandFailedException e)
            {
                logger.Info("adb server starts failed");
                logger.Warn(e);
                ui.Progress = 60;
                ui.LoadingTip = App.Current.Resources["ldmsgAdbServerFailed"].ToString();
                Thread.Sleep(10000);
                App.Current.Shutdown(e.ExitCode ?? 1);
            }

            ui.Progress = 60;
            ui.LoadingTip = App.Current.Resources["ldmsgLoadingExtensions"].ToString();
            OpenFrameworkManager.Init();
            OpenFxObserver.Instance.OnLoaded();
            ConnectedDevicesListener.Instance.Work();

            ui.Progress = 90;
            ui.LoadingTip = "How can a man die better?";
            Updater.CheckAndNotice();
            Statistics.Do();
            ToastMotd.Do();

            ui.Progress = 100;
            ui.LoadingTip = "Enjoy!";
            Thread.Sleep(1 * 1000);
            ui.Finish();
        }
#endregion
    }
}
