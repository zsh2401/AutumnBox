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
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Internal;
using AutumnBox.Support.Log;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI
{
    internal class AppLoader : Context
    {
        private readonly IAppLoadingWindow loadingWindowApi;
        public AppLoader(IAppLoadingWindow loadingWindowApi)
        {
            this.loadingWindowApi = loadingWindowApi;
        }
        public void PrintInfo()
        {
            Logger.Info(this, $"Run as " + (SystemHelper.HaveAdminPermission ? "Admin" : "Normal user"));
            Logger.Info(this, $"AutumnBox version: {SystemHelper.CurrentVersion}");
            Logger.Info(this, $"SDK version: {BuildInfo.SDK_VERSION}");
            Logger.Info(this, $"Windows version {Environment.OSVersion.Version}");
        }
        public async void LoadAsync()
        {
            //如果设置在启动时打开调试窗口
            if (Settings.Default.ShowDebuggingWindowNextLaunch)
            {
                //打开调试窗口
                new DebugWindow().Show();
            }
            PrintInfo();
            //启动ADB服务
            loadingWindowApi.SetProgress(10);
            loadingWindowApi.SetTip(App.Current.Resources["ldmsgStartAdb"].ToString());
            await Task.Run(() =>
            {
                bool success = false;
                bool tryAgain = true;
                while (!success)
                {
                    Logger.Info(this, "Try to start adb server ");
                    success = AdbHelper.StartServer();
                    Logger.Info(this, success ?  "adb server starts success": "adb server starts failed...");
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
            });
            //初始化主窗口
            App.Current.MainWindow = new MainWindow();
            //初始化拓展模块及其API框架
            loadingWindowApi.SetProgress(60);
            loadingWindowApi.SetTip(App.Current.Resources["ldmsgLoadingExtensions"].ToString());
            OpenFramewokManager.LoadApi();
            ScriptsManager.ReloadAll(this);
            ExtensionManager.LoadAllExtension(this);
            //启动设备拔插监听器
            loadingWindowApi.SetProgress(80);
            loadingWindowApi.SetTip(App.Current.Resources["ldmsgStartDeviceMonitor"].ToString());
            DevicesMonitor.Begin();
            //加载完成,启动主界面
            loadingWindowApi.SetProgress(100);
            loadingWindowApi.Finish();
            App.Current.MainWindow.Show();
        }
    }
}
