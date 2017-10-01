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
        VersionInfo info;
        public UpdateNoticeWindow(Window owner,VersionInfo info)
        {
            Owner = owner;
            this.info = info;
            InitializeComponent();
            this.ContentBox.Text = info.content;
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void buttonClose_Click(object sender, RoutedEventArgs e)
        {
            if (CheckBoxSkip.IsChecked == true) {
                Config.SkipVersion = info.version;
            }
            this.Close();
        }

        private void buttonGithubReleaseDownload_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe",info.githubReleaseDownloadUrl);
        }

        private void buttonBaiduPanDownlod_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", info.baiduPanDownloadUrl);
        }
    }
}
