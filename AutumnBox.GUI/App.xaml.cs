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
using AutumnBox.GUI.Util.Custom;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.Support.Log;
using System.Windows;

namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App() : base()
        {
            Current = this;
            AlreadyHaveAutumnBoxChecker.Do();
        }

        public static new App Current { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
#if !DEBUG
            this.DispatcherUnhandledException += FatalHandler.Current_DispatcherUnhandledException;
#endif
            ThemeManager.Instance.ApplyBySetting();
            if (Settings.Default.IsFirstLaunch)
            {
                Logger.Info(this, "is first launch");
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
            }
            Settings.Default.Save();
        }
    }
}
