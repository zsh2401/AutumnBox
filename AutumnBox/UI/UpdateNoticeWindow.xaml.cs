using AutumnBox.NetUtil;
using AutumnBox.Util;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    /// <summary>
    /// UpdateNoticeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateNoticeWindow : Window
    {
        string baiduUrl;
        string githubUrl;
        string version;
        public UpdateNoticeWindow(Window owner, UpdateCheckFinishedEventArgs e)
        {
            InitializeComponent();
            LH.Content = e.Header;
            TextBoxContent.Text = e.Message;
            baiduUrl = e.BaiduPanUrl;
            version = e.Version;
            githubUrl = e.GithubReleaseUrl;
            Owner = owner;
        }
        public static void FastShow(Window owner, UpdateCheckFinishedEventArgs e)
        {
            new UpdateNoticeWindow(owner, e).ShowDialog();
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxSkip.IsChecked == true)
            {
                Config.SkipVersion = version;
            }
            this.Close();
        }

        private void buttonGithubReleaseDownload_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(githubUrl);
        }

        private void buttonBaiduPanDownlod_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(baiduUrl);
        }
    }
}
