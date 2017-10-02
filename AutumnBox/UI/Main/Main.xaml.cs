using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.Debug;
using AutumnBox.Helper;
using AutumnBox.UI;
using AutumnBox.Util;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            Log.InitLogFile();
            Log.d(TAG, "Log Init Finish,Start Init Window");
            InitializeComponent();
            //buttonChangeTheme.click
            CustomInit();
        }

        private void DevicesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.DevicesListBox.SelectedIndex != -1)//如果选择了设备
            {
                new Thread(() =>
                {
                    Refresh();
                }).Start();
                ShowRateBox();
            }
            else
            {
                this.AndroidVersionLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                this.CodeLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                this.ModelLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                ChangeButtonByStatus(DeviceStatus.NO_DEVICE);
                ChangeImageByStatus(DeviceStatus.NO_DEVICE);
            }
        }

        /// <summary>
        /// 各方面加载完毕,毫秒毫秒钟就要开始渲染了!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            App.devicesListener.Start();//开始设备监听
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            if (Config.IsFirstLaunch)
            {
                MMessageBox.ShowDialog(this, FindResource("Notice2").ToString(), FindResource("FristLaunchNotice").ToString());
                Config.IsFirstLaunch = false;
            }
            BlurHelper.EnableBlur(this);
            GetNotice();//开始获取公告
            UpdateCheck();//更新检测
            InitWebPage();//初始化浏览器
        }

    }
}
