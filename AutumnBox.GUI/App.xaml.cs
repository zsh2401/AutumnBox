/* =============================================================================*\
*
* Filename: App.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 7/31/2017 05:34:44(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Adb;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.Log;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private const string TAG = "App";
        public const int HAVE_OTHER_PROCESS = 25364;
        public static new App Current { get; private set; }
        public MainWindow MainWindowAsMainWindow
        {
            get
            {
                return (Current.MainWindow as MainWindow);
            }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Support.Log.Logger.Info(this, "Startup");
            Current = this;
#if !DEBUG
            App.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
#endif
            if (SystemHelper.HaveOtherAutumnBoxProcess)
            {
                Logger.Debug(this, "have other autumnbox show MMessageBox and exit(1)");
                MessageBox.Show($"不可以同时打开两个AutumnBox{Environment.NewLine}Do not run two AutumnBox at once", "警告/Warning");
                App.Current.Shutdown(HAVE_OTHER_PROCESS);
            }
            if (Config.IsFirstLaunch)
            {
                LanguageHelper.SetLanguageByEnvironment();
            }
            else
            {
                LanguageHelper.LoadLanguageByFileName(Config.Lang);
            }
        }

        internal async void Load(IAppLoadingWindow loadingWindowApi)
        {
            Debug.WriteLine("Loading...", TAG);
            loadingWindowApi.SetProgress(10);
            loadingWindowApi.SetTip("启动ADB服务中....");
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
                        App.Current.Shutdown(HAVE_OTHER_PROCESS);
                    }
                }
            });
            loadingWindowApi.SetProgress(50);
            await Task.Run(() =>
            {
                Thread.Sleep(2000);
            });
            App.Current.MainWindow = new MainWindow();
            DevicesMonitor.Begin();
            loadingWindowApi.SetProgress(80);
            if (Config.ShowDebugWindowOnNextLaunch)
            {
                new DebugWindow().Show();
            }
            ExtendModuleManager.Load();
            App.Current.MainWindow.Show();
            loadingWindowApi.SetProgress(100);
            loadingWindowApi.Finish();
        }
        private string[] blockListForExceptionSource = {
            "PresentationCore"
        };
        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            string src = e.Exception.Source;
            if (blockListForExceptionSource.Contains(src)) return;
            MessageBox.Show(
                $"一个未知的错误的发生了,将logs文件夹压缩并发送给开发者以解决问题{Environment.NewLine}Please compress the logs folder and send it to zsh2401@163.com",
                "AutumnBox 错误/Unknow Exception",
            MessageBoxButton.OK,
            MessageBoxImage.Warning);
            string n = Environment.NewLine;
            string exstr =
                $"AutumnBox Exception {DateTime.Now.ToString("MM/dd/yyyy    HH:mm:ss")}{n}{n}" +
                $"Exception:{n}{e.Exception.ToString()}{n}{n}{n}" +
                $"Message:{n}{e.Exception.Message}{n}{n}{n}" +
                $"Source:{n}{e.Exception.Source}{n}{n}{n}" +
                $"Inner:{n}{e.Exception.InnerException?.ToString() ?? "None"}{n}";

            if (!Directory.Exists("logs"))
            {
                Directory.CreateDirectory("logs");
            }
            File.WriteAllText("logs\\exception.log", exstr);
            e.Handled = true;
            App.Current.Shutdown(1);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Logger.Info(this, "Exit code : " + e.ApplicationExitCode);
            if (e.ApplicationExitCode != HAVE_OTHER_PROCESS)
            {
                AdbHelper.KillAllAdbProcess();
            }
            if (Config.IsFirstLaunch)
            {
                Config.IsFirstLaunch = false;
            }
        }
    }
}
