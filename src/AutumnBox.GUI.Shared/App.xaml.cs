//#define STRICT_CHECK
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
*
\* =============================================================================*/
using AutumnBox.GUI.Resources.Languages;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Services.Impl.OS;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Loader;
using AutumnBox.Leafx;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// AppLoader被创建了
        /// </summary>
        internal event EventHandler<AppLoaderCreatedEventArgs> AppLoaderCreated;
        /// <summary>
        /// AppLoader创建事件
        /// </summary>
        internal class AppLoaderCreatedEventArgs : EventArgs
        {
            public AppLoaderCreatedEventArgs(AbstractAppLoader appLoader)
            {
                AppLoader = appLoader ?? throw new ArgumentNullException(nameof(appLoader));
            }

            public AbstractAppLoader AppLoader { get; }
        }
        /// <summary>
        /// 构造应用
        /// </summary>
        public App() : base()
        {
            Current = this;
            Thread.CurrentThread.Name = "Application Main Thread";
        }

        /// <summary>
        /// 获取全局湖对象
        /// </summary>
        public IRegisterableLake Lake { get; private set; }

        /// <summary>
        /// 获取当前的应用
        /// </summary>
        public static new App Current { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
#if DEBUG && STRICT_CHECK
            if (!Lang.FileCheck())
            {
                MessageBox.Show("Language File Error! See details in debug output!",
                    "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
#endif
            Lake = new SunsetLake();
            ScanComponents();
            this.GetComponent<IThemeManager>().Reload();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            var appLoader = new GeneralAppLoader();
            AppLoaderCreated?.Invoke(this, new AppLoaderCreatedEventArgs(appLoader));
            _ = appLoader.LoadAsync();
        }
        private void ScanComponents()
        {
            new ClassComponentsLoader(
            "AutumnBox.GUI.Services.Impl", Lake).Do();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            AppUnloader.Unload();
            base.OnExit(e);
        }
    }
}
