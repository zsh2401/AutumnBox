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
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using System.Globalization;

namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        internal AbstractAppLoader AppLoader { get; }

        /// <summary>
        /// 获取全局湖对象
        /// </summary>
        public IRegisterableLake Lake { get; private set; }

        /// <summary>
        /// 获取当前的应用
        /// </summary>
#pragma warning disable CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。
        public static new App Current { get; private set; }
#pragma warning restore CS8618 // 不可为 null 的字段未初始化。请考虑声明为可以为 null。

        /// <summary>
        /// 构造应用
        /// </summary>
        public App() : base()
        {
            Current = this;
            Thread.CurrentThread.Name = "Application Main Thread";
            AppLoader = new GeneralAppLoader();
            Lake = new SunsetLake();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureAppCenter();
#if DEBUG && STRICT_CHECK
            if (!Lang.FileCheck())
            {
                MessageBox.Show("Language File Error! See details in debug output!",
                    "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
#endif
            ScanComponents();
#pragma warning disable CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
            AppLoader.LoadAsync();
#pragma warning restore CS4014 // 由于此调用不会等待，因此在调用完成前将继续执行当前方法
        }

        private void ConfigureAppCenter()
        {
            DispatcherUnhandledException += (s, e) =>
              {
                  Crashes.TrackError(e.Exception);
              };
            AppCenter.SetCountryCode(RegionInfo.CurrentRegion.TwoLetterISORegionName);
            //It's a secret, you see nothing.
            AppCenter.Start("c41ffd94-f04a-47e9-b43f-6ecca159e3c7", typeof(Analytics), typeof(Crashes));
            //Crashes.GenerateTestCrash();
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
