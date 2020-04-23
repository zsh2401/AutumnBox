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
using AutumnBox.Leafx;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
namespace AutumnBox.GUI
{

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public const int ERR_BANNED_VERSION = 2501;

        /// <summary>
        /// 构造应用
        /// </summary>
        public App() : base()
        {
            Current = this;
            FrameworkElement.StyleProperty.OverrideMetadata(typeof(Window), new FrameworkPropertyMetadata
            {
                DefaultValue = FindResource(typeof(Window))
            });
            Thread.CurrentThread.Name = "Application Main Thread";
        }

        /// <summary>
        /// 获取全局湖对象
        /// </summary>
        public IRegisterableLake Lake => (IRegisterableLake)GLake.Lake;

        /// <summary>
        /// 获取当前的应用
        /// </summary>
        public static new App Current { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            if (!CheckOther(e.Args))
            {
                Shutdown(0);
                return;
            };
#if DEBUG && STRICT_CHECK
            if (!Lang.FileCheck())
            {
                MessageBox.Show("Language File Error! See details in debug output!",
                    "Error!",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Shutdown(1);
            }
#endif
            LoadComponent();
            this.GetComponent<IThemeManager>().Reload();
            base.OnStartup(e);
        }
        private bool CheckOther(string[] args)
        {
            var process = OtherProcessChecker.ThereIsOtherAutumnBoxProcess();
            if (process != null && (!args.Contains("--wait")))
            {
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                App.Current.Shutdown(0);
                return false;
            }
            process?.WaitForExit();
            return true;
        }

        /// <summary>
        /// 将AutumnBox.GUI的部分组件加载到Leafx框架中
        /// </summary>
        private void LoadComponent()
        {
            new ClassComponentsLoader(
                "AutumnBox.GUI.Services.Impl",
                Current.Lake)
                .Do();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AppUnloader.Instance.Unload();
        }
    }
}
