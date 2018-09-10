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
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Support.Log;
using System;
using System.Windows;

namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        internal const int HAVE_OTHER_PROCESS = 25364;

        public App() : base()
        {
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
            this.DispatcherUnhandledException += FatalHandler.Current_DispatcherUnhandledException;
#endif
            if (Settings.Default.SkipVersion == "0.0.1")
            {
                Settings.Default.SkipVersion = Self.Version.ToString();
                Settings.Default.Save();
            }
            if (Self.HaveOtherAutumnBoxProcess)
            {
                Logger.Fatal(this, "Have other autumnbox!!");
                MessageBox.Show(
                    $"不可以同时打开两个AutumnBox{Environment.NewLine}Do not run two AutumnBox at once",
                    "警告/Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Shutdown(HAVE_OTHER_PROCESS);
            }
            if (Settings.Default.IsFirstLaunch)
            {
                LanguageManager.Instance.ApplyByEnvoriment();
            }
            else
            {
                LanguageManager.Instance.ApplyByLanguageCode(Settings.Default.Language);
            }
        }


        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Logger.Info(this, "Exit code : " + e.ApplicationExitCode);
            if (Settings.Default.IsFirstLaunch)
            {
                Settings.Default.IsFirstLaunch = false;
                Settings.Default.Save();
            }
            Settings.Default.Save();
        }
    }
}
