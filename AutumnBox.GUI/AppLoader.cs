/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/17 23:47:47 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Adb;
using AutumnBox.Basic.MultipleDevices;
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
        private IAppLoadingWindow loadingWindowApi;
        public AppLoader(IAppLoadingWindow loadingWindowApi)
        {
            this.loadingWindowApi = loadingWindowApi;
            System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
            Logger.Info(this, $"is admin?{principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator)}");
        }
        public async void LoadAsync()
        {
            if (Settings.Default.ShowDebuggingWindowNextLaunch)
            {
                new DebugWindow().Show();
            }
            Logger.Info(this, "Loading...");
            loadingWindowApi.SetProgress(10);
            loadingWindowApi.SetTip(App.Current.Resources["ldmsgStartAdb"].ToString());
            await Task.Run(() =>
            {
                bool success = false;
                bool tryAgain = true;
                while (!success)
                {
                    success = AdbHelper.StartServer();
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
                        App.Current.Shutdown(App.HAVE_OTHER_PROCESS);
                    }
                }
            });
            loadingWindowApi.SetProgress(60);
            loadingWindowApi.SetTip(App.Current.Resources["ldmsgStartDeviceMonitor"].ToString());
            App.Current.MainWindow = new MainWindow();
            DevicesMonitor.Begin();
            loadingWindowApi.SetProgress(80);
            loadingWindowApi.SetTip(App.Current.Resources["ldmsgLoadingExtensions"].ToString());
            OpenFramewokManager.LoadApi();
            ExtensionManager.LoadAllExtension(this);
            App.Current.MainWindow.Show();
            loadingWindowApi.SetProgress(100);
            loadingWindowApi.Finish();
        }
    }
}
