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
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.PaidVersion;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Internal;
using AutumnBox.Support.Log;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        internal const int HAVE_OTHER_PROCESS = 25364;

        internal FrameworkManager OpenFrameworkManager { get; private set; }

        internal IApplicationContext SpringContext { get; private set; }

        internal Context OpenFrameworkContext { get; private set; }

        private class AppContext : Context { }
#if PAID_VERSION
        internal readonly IAccountManager AccountManager;
        internal IAccount Account => AccountManager.Current;
#endif
        public App() :base(){
            OpenFrameworkContext = new AppContext();
            SpringContext = new XmlApplicationContext("AutumnBoxAop.atmbxml");
            OpenFrameworkManager = new FrameworkManager(OpenFrameworkContext);
            AccountManager = SpringContext.GetObject<IAccountManager>("accountManager");
        }

        public static new App Current { get; private set; }

        public MainWindow MainWindowAsMainWindow
        {
            get
            {
                return (Current.MainWindow as MainWindow);
            }
        }
        public string FormatResourceString(string key, params object[] args)
        {
            return String.Format(Resources[key].ToString(), args);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Current = this;

#if !DEBUG
            this.DispatcherUnhandledException += Current_DispatcherUnhandledException;
#endif
            if (Settings.Default.SkipVersion == "0.0.1")
            {
                Settings.Default.SkipVersion = SystemHelper.CurrentVersion.ToString();
                Settings.Default.Save();
            }
            if (SystemHelper.HaveOtherAutumnBoxProcess)
            {
                Logger.Fatal(this, "Have other autumnbox!!");
                MessageBox.Show(
                    $"不可以同时打开两个AutumnBox{Environment.NewLine}Do not run two AutumnBox at once",
                    "警告/Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown(HAVE_OTHER_PROCESS);
            }
            if (Settings.Default.IsFirstLaunch)
            {
                LanguageHelper.SetLanguageByEnvironment();
            }
            else
            {
                LanguageHelper.SetLanguage(Settings.Default.Language);
            }
        }

        private string[] blockListForExceptionSource = {
            "PresentationCore"
        };
        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string src = e.Exception.Source;
            if (blockListForExceptionSource.Contains(src))
            {
                Logger.Warn(this, "PresentationCore Error", e.Exception);
                return;
            }
            string n = Environment.NewLine;
            string exstr =
                $"AutumnBox Exception {DateTime.Now.ToString("MM/dd/yyyy    HH:mm:ss")}{n}{n}" +
                $"Exception:{n}{e.Exception.ToString()}{n}{n}" +
                $"Message:{n}{e.Exception.Message}{n}{n}" +
                $"Source:{n}{e.Exception.Source}{n}{n}" +
                $"Inner:{n}{e.Exception.InnerException?.ToString() ?? "None"}{n}";

            try { Logger.Fatal(this, exstr); } catch { }
            ShowErrorToUser(exstr);
            e.Handled = true;
            Shutdown(1);
        }

        private void ShowErrorToUser(string exstr)
        {
            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case "zh-CN":
                case "zh-TW":
                case "zh-SG":
                case "zh-HK":
                    try
                    {
                        new FatalWindow(exstr).ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show(
                                $"一个未知的错误的发生了,将logs文件夹压缩并发送给开发者以解决问题{Environment.NewLine}" +
                                $"出问题不发logs,开发者永远不可能解决你遇到的问题{Environment.NewLine}" +
                                 $"邮件/QQ: zsh2401@163.com{Environment.NewLine}",
                                "AutumnBox 错误",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                    break;
                default:
                    MessageBox.Show(
                         $"AutumnBox was failed on running{Environment.NewLine}" +
                        $"Please compress the logs folder and send it to zsh2401@163.com",
                        "Unknow Exception",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                    break;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Logger.Info(this, "Exit code : " + e.ApplicationExitCode);
            if (e.ApplicationExitCode != HAVE_OTHER_PROCESS)
            {
                AdbHelper.KillAllAdbProcess();
            }
            if (Settings.Default.IsFirstLaunch)
            {
                Settings.Default.IsFirstLaunch = false;
                Settings.Default.Save();
            }
            Settings.Default.Save();
        }
    }
}
