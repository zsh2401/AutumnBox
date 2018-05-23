using AutumnBox.GUI.Helper;
using AutumnBox.Support.Log;
using System.Windows;
using System.Windows.Input;
using AutumnBox.GUI.UI.Fp;
#if PAID_VERSION
using AutumnBox.GUI.PaidVersion;
#endif
namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// AppLoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppLoadingWindow : Window, IAppLoadingWindow
    {
        private readonly LoginPanel loginPanel;
        public AppLoadingWindow()
        {
            InitializeComponent();
            ThemeManager.LoadFromSetting();
#if PAID_VERSION
            loginPanel = new LoginPanel();
#endif
        }

        public void SetProgress(double value)
        {
            PrgBar.Value = value;
        }

        public void SetTip(string keyOrValue)
        {
            TBTip.Text = UIHelper.GetString(keyOrValue);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void Finish()
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if PAID_VERSION
            new FastPanel(this.GridBase, loginPanel).Display();
            Logger.Debug(this,"Fast panel");
            new AppLoader(this, loginPanel).LoadAsync();
#else
              new AppLoader(this).LoadAsync();
#endif
        }
    }
}
