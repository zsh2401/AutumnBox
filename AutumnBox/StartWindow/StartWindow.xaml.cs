using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.Debug;
using AutumnBox.Helper;
using AutumnBox.NetUtil;
using AutumnBox.UI;
using AutumnBox.Util;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
            Log.InitLogFile();
            Log.d(TAG, "Log Init Finish,Start Init Window");
            InitializeComponent();
            App.devicesListener.DevicesChanged += DevicesChanged;
            Log.d(TAG, "Start customInit");
            ChangeButtonByStatus(DeviceStatus.NO_DEVICE);//将所有按钮设置成关闭状态
            ChangeImageByStatus(DeviceStatus.NO_DEVICE);
#if DEBUG
            this.labelTitle.Content += "  " + StaticData.nowVersion.version + "-Debug";
#else
            this.labelTitle.Content += "  " + StaticData.nowVersion.version + "-Release";
#endif
        }
        /// <summary>
        /// 当设备监听器引发连接设备变化的事件时发生,可通过此事件获取最新的连接设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevicesChanged(object sender, DevicesChangeEventArgs e) {
                Log.d(TAG, "Devices change handing.....");
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
                MMessageBox.ShowDialog(this, FindResource("Notice2").ToString(), FindResource("FristLaunchNotice").ToString());
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
            new Thread(() =>
            {
                Guider guider = new Guider();
                if (guider.isOk)
                {
                    try
                    {
                        this.Dispatcher.Invoke(new Action(() =>
                        {
                            webFlashHelper.Navigate(guider["urls"]["flashhelp"].ToString());
                            webSaveDevice.Navigate(guider["urls"]["savedevicehelp"].ToString());
                            webFlashRecHelp.Navigate(guider["urls"]["flashrecoveryhelp"].ToString());
                        }));
                    }
                    catch (Exception e)
                    {
                        Log.d(TAG, "web browser set fail");
                        Log.d(TAG, e.Message);
                    }
                }
                else
                {
                    Log.d(TAG, "web browser set fail because guider is not ok");
                }
            }).Start();
        }
    }
}
