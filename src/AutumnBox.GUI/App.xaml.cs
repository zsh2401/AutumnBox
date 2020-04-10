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
using AutumnBox.GUI.Util.Theme;
using AutumnBox.Leafx;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.OpenFramework.Open;
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
            this.GetType();
        }

        public static new App Current { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            LoadComponent();
            GLake.Lake.Get<IThemeManager>().Reload();
            base.OnStartup(e);
        }

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
