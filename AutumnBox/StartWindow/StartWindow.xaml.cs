/* =============================================================================*\
*
* Filename: StartWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 10/6/2017 03:31:15(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Function.RunningManager;
using AutumnBox.Helper;
using AutumnBox.NetUtil;
using AutumnBox.UI;
using AutumnBox.Util;
using AutumnBox.Windows;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows;
using static AutumnBox.Helper.UIHelper;
namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        string TAG = "MainWindow";
        //ResourceDictionary Resources = App.Current.Resources;
        public StartWindow()
        {
            Logger.InitLogFile();
            Logger.D(TAG, "Log Init Finish,Start Init Window");
            InitializeComponent();
            App.devicesListener.DevicesChanged += DevicesChanged;
            Logger.D(TAG, "Start customInit");
            
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
        private void DevicesChanged(object sender, DevicesChangedEventArgs e)
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
            RefreshUI();
            App.devicesListener.Start();//开始设备监听
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            if (Config.IsFirstLaunch)
            {
                MMessageBox.ShowDialog( FindResource("Notice2").ToString(), App.Current.Resources["msgFristLaunchNotice"].ToString());
                Config.IsFirstLaunch = false;
            }
            //BlurHelper.EnableBlur(this);
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
                        new UpdateNoticeWindow(e).ShowDialog();
                    });
                }
            });
        }
        void InitWebPage()
        {
            webFlashHelper.Navigate(HelpUrl.flashhelp);
            webSaveDevice.Navigate(HelpUrl.savedevice);
            webFlashRecHelp.Navigate(HelpUrl.flashrecovery);
        }

        private void ButtonSideload_Click(object sender, RoutedEventArgs e)
        {
            if (App.SelectedDevice.Status != DeviceStatus.SIDELOAD) {
                MMessageBox.ShowDialog( GetString("Warning"),GetString(""));
            }
        }

        private void ButtonInstallApk_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "安卓安装包(*.apk)|*.apk";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                //FileSender fs = new FileSender(new FileArgs { files = new string[] { fileDialog.FileName } });
                //RunningManager rm = App.SelectedDevice.GetRunningManger(fs);
                //rm.FuncEvents.Finished += FuncFinish;
                //rm.FuncStart();
                //new FileSendingWindow(this, rm).ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void ButtonScreentShot_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMiFlash_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
