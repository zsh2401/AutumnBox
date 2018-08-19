using System.Windows;
using System.Windows.Input;
using AutumnBox.GUI.Util.UI;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// AppLoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppLoadingWindow : Window/*, IAppLoadingWindow*/
    {
        public AppLoadingWindow()
        {
            InitializeComponent();
            //LanguageManager.Instance.ApplyByEnvoriment();
            //ThemeManager.LoadFromSetting();
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
              //new AppLoader(this).LoadAsync();
        }
    }
}
