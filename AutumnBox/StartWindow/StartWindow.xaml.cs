using AutumnBox.Basic.Devices;
using AutumnBox.Helper;
using AutumnBox.NetUtil;
using AutumnBox.UI;
using AutumnBox.Util;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        string TAG = "MainWindow";

        public StartWindow()
        {
            Logger.InitLogFile();
            Logger.D(TAG, "Log Init Finish,Start Init Window");
            InitializeComponent();
            App.devicesListener.DevicesChanged += DevicesChanged;
            Logger.D(TAG, "Start customInit");
            ChangeButtonByStatus(DeviceStatus.NO_DEVICE);//将所有按钮设置成关闭状态
            ChangeImageByStatus(DeviceStatus.NO_DEVICE);
#if DEBUG
            LabelVersion.Content = StaticData.nowVersion.version + "-Debug";
            labelTitle.Content += "  " + StaticData.nowVersion.version + "-Debug";
#else
            LabelVersion.Content = StaticData.nowVersion.version + "-Release";
            labelTitle.Content += "  " + StaticData.nowVersion.version + "-Release";
#endif
        }
        /// <summary>
        /// 当设备监听器引发连接设备变化的事件时发生,可通过此事件获取最新的连接设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevicesChanged(object sender, DevicesChangeEventArgs e)
        {
            Logger.D(TAG, "Devices change handing.....");
            this.Dispatcher.Invoke(() =>
            {
                DevicesListBox.ItemsSource = e.DevicesList;
                if (e.DevicesList.Count == 1)
                {
                    DevicesListBox.SelectedIndex = 0;
                }
            });
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
                MMessageBox.ShowDialog(this, FindResource("Notice2").ToString(), App.Current.Resources["msgFristLaunchNotice"].ToString());
                Config.IsFirstLaunch = false;
            }
            BlurHelper.EnableBlur(this);
            GetNotice();//开始获取公告
            UpdateCheck();//更新检测
            InitWebPage();//初始化浏览器
        }
        void GetNotice()
        {
            new MOTDGetter().Run((s, e) =>
            {
                textBoxGG.Dispatcher.Invoke(() =>
                {
                    textBoxGG.Text = e.Header + " : " + e.Message;
                });
            });
        }
        void UpdateCheck()
        {
            new UpdateChecker().Run((s, e) =>
            {
                if (e.NeedUpdate)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        new UpdateNoticeWindow(this, e).ShowDialog();
                    });
                }
            });
        }
        void InitWebPage()
        {
            webFlashHelper.Navigate(HelpUrl.flashhelp);
            webSaveDevice.Navigate(HelpUrl.savedevice);
            webFlashRecHelp.Navigate(HelpUrl.flashrecovery);
            //new Thread(() =>
            //{

            //Guider guider = new Guider();
            //    if (guider.isOk)
            //    {
            //        try
            //        {
            //            this.Dispatcher.Invoke(new Action(() =>
            //            {
            //                webFlashHelper.Navigate(guider["urls"]["flashhelp"].ToString());
            //                webSaveDevice.Navigate(guider["urls"]["savedevicehelp"].ToString());
            //                webFlashRecHelp.Navigate(guider["urls"]["flashrecoveryhelp"].ToString());
            //            }));
            //        }
            //        catch (Exception e)
            //        {
            //            Logger.D(TAG, "web browser set fail");
            //            Logger.D(TAG, e.Message);
            //        }
            //    }
            //    else
            //    {
            //        Logger.D(TAG, "web browser set fail because guider is not ok");
            //    }
            //}).Start();
        }
    }
}
