/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/19 20:39:02 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.OpenFxManagement;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util
{
    class AppLoader
    {
        public interface ILoadingUI
        {
            string LoadingTip { set; }
            double Progress { set; }
            void Finish();
        }
        private readonly ILoadingUI ui;
        public AppLoader(ILoadingUI ui)
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
        private void Load()
        {
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
            Logger.Info(this, $"Run as " + (Self.HaveAdminPermission ? "Admin" : "Normal user"));
            Logger.Info(this, $"AutumnBox version: {Self.Version}");
            Logger.Info(this, $"SDK version: {BuildInfo.SDK_VERSION}");
            Logger.Info(this, $"Windows version {Environment.OSVersion.Version}");
            ui.Progress = 30;
            ui.LoadingTip = App.Current.Resources["ldmsgStartAdb"].ToString();
            bool success = false;
            bool tryAgain = true;
            while (!success)
            {
                Logger.Info(this, "Try to start adb server ");
                success = AdbHelper.StartServer();
                Logger.Info(this, success ? "adb server starts success" : "adb server starts failed...");
                if (!success)
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        tryAgain = BoxHelper.ShowChoiceDialog(
                        "msgWarning",
                        "msgStartAdbServerFail",
                        "btnExit", "btnIHaveCloseOtherPhoneHelper").ToBool();
                    });
                if (tryAgain)
                {
                    Thread.Sleep(2000);
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        App.Current.Shutdown(App.HAVE_OTHER_PROCESS);
                    });
                }
            }
            ui.Progress = 60;
            ui.LoadingTip = App.Current.Resources["ldmsgLoadingExtensions"].ToString();
            OpenFrameworkManager.Init();
            OpenFxObserver.Instance.OnLoaded();
            ui.Progress = 100;
            ui.LoadingTip = "Enjoy!";
            ConnectedDevicesListener.Instance.Work();
            Thread.Sleep(1 * 1000);
            ui.Finish();
        }
    }
}
