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
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Net;
using System;
using System.Windows;
namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public const int ERR_BANNED_VERSION = 2501;
        public App() : base()
        {
            Current = this;
        }

        public static new App Current { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            AppLoader.Instance.Failed += (s, _e) =>
            {
                Shutdown(1);
            };
            AppLoader.Instance.LoadAsync();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AppUnloader.Instance.Unload();
            if (e.ApplicationExitCode == ERR_BANNED_VERSION)
            {
                string banned = Resources["CurrentVersionHasBeenBanned"].ToString();
                string message = $"{banned}{Environment.NewLine}{Banner.Reason}";
                MessageBox.Show(message, banned, MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
