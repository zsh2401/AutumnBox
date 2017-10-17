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
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using static AutumnBox.Helper.UIHelper;
namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window
    {
        string TAG = "MainWindow";
        private Object setUILock = new System.Object();
        public StartWindow()
        {
            Logger.InitLogFile();
            Logger.D(TAG, "Log Init Finish,Start Init Window");
            InitializeComponent();
            App.DevicesListener.DevicesChanged += DevicesChanged;
            TitleBar.OwnerWindow = this;
            TitleBar.ImgMin.Visibility = Visibility.Visible;
            DevInfoPanel.RefreshStart += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "RefreshStart..");
                    UIHelper.ShowRateBox();
                });
            };
            DevInfoPanel.RefreshFinished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "RefreshFinished..");
                    UIHelper.CloseRateBox();
                });
            };
#if DEBUG
            AboutControl.LabelVersion.Content = DebugInfo.NowVersion + "-Debug";
            TitleBar.Title.Content += "  " + DebugInfo.NowVersion + "-Debug";
#else
            LabelVersion.Content = StaticData.nowVersion.version + "-Release";
            TitleBar.Title.Content += "  " + StaticData.nowVersion.version + "-Release";
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
            App.DevicesListener.Start();//开始设备监听
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            if (Config.IsFirstLaunch)
            {
                MMessageBox.ShowDialog(FindResource("Notice2").ToString(), App.Current.Resources["msgFristLaunchNotice"].ToString());
                Config.IsFirstLaunch = false;
            }
            GetNotice();//开始获取公告
            UpdateCheck();//更新检测
            InitWebPage();//初始化浏览器
        }
        /// <summary>
        /// 当窗口关闭时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
            App.DevicesListener.Stop();
            Basic.Executer.CommandExecuter.Kill();
            Environment.Exit(0);
        }
        /// <summary>
        /// 根据当前选中的设备刷新界面信息
        /// </summary>
        private void RefreshUI()
        {
            lock (setUILock)
            {
                SetButtons();
                DevInfoPanel.Refresh(App.SelectedDevice);
            }
        }
        /// <summary>
        /// 根据设备状态改变按钮状态
        /// </summary>
        /// <param name="status"></param>
        private void SetButtons()
        {
            bool inBootLoader = false;
            bool inRecovery = false;
            bool inRunning = false;
            bool notFound = false;
#pragma warning disable CS0219 // 变量已被赋值，但从未使用过它的值
            bool inSideload = false;
#pragma warning restore CS0219 // 变量已被赋值，但从未使用过它的值
            switch (App.SelectedDevice.Status)
            {
                case DeviceStatus.FASTBOOT:
                    inBootLoader = true;
                    break;
                case DeviceStatus.RECOVERY:
                    inRecovery = true;
                    break;
                case DeviceStatus.RUNNING:
                    inRunning = true;
                    break;
                case DeviceStatus.SIDELOAD:
                    inSideload = true;
                    break;
                default:
                    notFound = true;
                    break;
            }
            SetGridButtonStatus(PoweronFuncs, inRunning);
            SetGridButtonStatus(RecoveryFuncs, inRecovery);
            SetGridButtonStatus(FastbootFuncs, inBootLoader);
            this.RebootGrid.buttonRebootToBootloader.IsEnabled = !notFound;
            this.RebootGrid.buttonRebootToSystem.IsEnabled = !notFound;
            this.RebootGrid.buttonRebootToRecovery.IsEnabled = (inRunning || inRecovery);
        }
        /// <summary>
        /// 当设备选择列表的被选项变化时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevicesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DevicesListBox.SelectedIndex != -1)//如果选择了设备
            {
                App.SelectedDevice = ((DeviceSimpleInfo)DevicesListBox.SelectedItem);
            }
            else if (this.DevicesListBox.SelectedIndex == -1)
            {
                App.SelectedDevice = new DeviceSimpleInfo() { Status = DeviceStatus.NO_DEVICE };
            }
            RefreshUI();
        }
        /// <summary>
        /// 获取公告
        /// </summary>
        void GetNotice()
        {
            var getter = new MOTDGetter();
            getter.GetFinished += (s, e) =>
            {
                Dispatcher.Invoke(() =>
                {
                    textBoxGG.Dispatcher.Invoke(() =>
                    {
                        textBoxGG.Text = e.Header + " : " + e.Message;
                    });
                });
            };
            getter.Run();
        }
        /// <summary>
        /// 更新检测
        /// </summary>
        void UpdateCheck()
        {
            var checker = new UpdateChecker();
            checker.CheckFinished += (s, e) =>
            {
                if (e.NeedUpdate)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        new UpdateNoticeWindow(e).ShowDialog();
                    });
                }
            };
        }
        /// <summary>
        /// 初始化帮助网页
        /// </summary>
        void InitWebPage()
        {
            webFlashHelper.Navigate(Urls.HELP_PAGE);
        }
    }
}
