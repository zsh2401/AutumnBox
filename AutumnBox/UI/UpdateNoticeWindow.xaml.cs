using AutumnBox.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
