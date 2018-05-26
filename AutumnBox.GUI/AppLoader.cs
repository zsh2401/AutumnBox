/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/17 23:47:47 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.PaidVersion;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.Support.Log;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutumnBox.GUI
{
    internal class AppLoader : Context
    {
        private readonly IAppLoadingWindow loadingWindowApi;

#if PAID_VERSION
        private readonly ILoginUX loginUX;
        public AppLoader(IAppLoadingWindow loadingWindow, ILoginUX loginUX)
        {
            this.loginUX = loginUX;
            this.loadingWindowApi = loadingWindow;
        }
#else
        public AppLoader(IAppLoadingWindow loadingWindowApi)
        {
            this.loadingWindowApi = loadingWindowApi;
        }
#endif
        private void PrintInfo()
        {
            Logger.Info(this, $"Run as " + (SystemHelper.HaveAdminPermission ? "Admin" : "Normal user"));
            Logger.Info(this, $"AutumnBox version: {SystemHelper.CurrentVersion}");
            Logger.Info(this, $"SDK version: {BuildInfo.SDK_VERSION}");
            Logger.Info(this, $"Windows version {Environment.OSVersion.Version}");
        }
#if PAID_VERSION
        private void Login()
        {
            var am = App.Current.AccountManager;
            App.Current.Dispatcher.Invoke(() =>
            {
                am.Init();
                am.UX = loginUX;
            });
            am.Login();
        }
#endif
        public async void LoadAsync()
        {
            await Task.Run(() => { Load(); });
        }

        public void Load()
        {
#if PAID_VERSION
            App.Current.Dispatcher.Invoke(() =>
            {
                loadingWindowApi.SetProgress(10);
                loadingWindowApi.SetTip(App.Current.Resources["ldmsgLoginAccount"].ToString());
            });
            Login();
            Updater.DeleteUpdaterTemp();
            var r = Updater.Check();
            if (r.NeedUpdate)
            {
                bool exit = false;
                App.Current.Dispatcher.Invoke(() =>
                {
                    var gotoU = MessageBox.Show("检测到更新,是否更新?" + 
                        Environment.NewLine + 
                        r.Content, "更新检测", MessageBoxButtons.OKCancel);
                    if (gotoU == DialogResult.OK)
                    {
                        exit = gotoU == DialogResult.OK;
                        Updater.RunUpdater();
                    }
                });
                if (exit) {
                    App.Current.Dispatcher.Invoke(()=> {
                        App.Current.Shutdown();
                    });
                    return;
                }
            }
#endif
            //如果设置在启动时打开调试窗口
            if (Settings.Default.ShowDebuggingWindowNextLaunch)
            {
                //打开调试窗口
                App.Current.Dispatcher.Invoke(() =>
                {
                    new DebugWindow().Show();
                });
            }
            PrintInfo();
            //启动ADB服务
            App.Current.Dispatcher.Invoke(() =>
            {
                loadingWindowApi.SetProgress(30);
                loadingWindowApi.SetTip(App.Current.Resources["ldmsgStartAdb"].ToString());
            });

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
            App.Current.Dispatcher.Invoke(() =>
            {
                //初始化主窗口
                App.Current.MainWindow = new MainWindow();
                //初始化拓展模块及其API框架
                loadingWindowApi.SetProgress(60);
                loadingWindowApi.SetTip(App.Current.Resources["ldmsgLoadingExtensions"].ToString());
            });
            OpenFrameworkManager.LoadApi();
            App.Current.OpenFrameworkManager.ReloadAllScript();
            App.Current.OpenFrameworkManager.LoadAllExtension();

            //启动设备拔插监听器
            App.Current.Dispatcher.Invoke(() =>
            {
                loadingWindowApi.SetProgress(80);
                loadingWindowApi.SetTip(App.Current.Resources["ldmsgStartDeviceMonitor"].ToString());
            });
            DevicesMonitor.Begin();

            //加载完成,启动主界面
            App.Current.Dispatcher.Invoke(() =>
            {
                loadingWindowApi.SetProgress(100);
                loadingWindowApi.Finish();
                App.Current.MainWindow.Show();
            });
        }
    }
}