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
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Theme;
using AutumnBox.Leafx;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
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
            LoadComponent();
            GLake.Lake.Get<IThemeManager>().Reload();
            base.OnStartup(e);
        }

        /// <summary>
        /// 将AutumnBox.GUI的部分组件加载到Leafx框架中
        /// </summary>
        private void LoadComponent()
        {
            new ComponentFactoryReader((IRegisterableLake)GLake.Lake, typeof(ComponentFactory));
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            AppUnloader.Instance.Unload();
        }
    }
}
