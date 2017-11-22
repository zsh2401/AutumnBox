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
using AutumnBox.Basic.Adb;
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI
{
    [LogProperty(TAG = "Main Window")]
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class StartWindow : Window, ILogSender
    {
        private Object setUILock = new System.Object();

        public string LogTag => "Main Window";
        public bool IsShowLog => true;
        public StartWindow()
        {
            InitializeComponent();
            App.DevicesListener.DevicesChanged += DevicesChanged;
            TitleBar.OwnerWindow = this;
            TitleBar.ImgMin.Visibility = Visibility.Visible;
            DevInfoPanel.RefreshStart += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "Refresh Start..");
                    UIHelper.ShowRateBox();
                });
            };
            FunctionModule.AnyFinished += this.FuncFinish;
            DevInfoPanel.RefreshFinished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "Refresh UI Finished..");
                    UIHelper.CloseRateBox();
                });
            };
            AdbHelper.AdbServerStartsFailed += (s, e) =>
            {
                App.DevicesListener.Stop();
                bool _continue = true;
                Dispatcher.Invoke(() =>
                {

                    _continue = ChoiceBox.FastShow(this, UIHelper.GetString("msgWarning"),
                           UIHelper.GetString("msgStartAdbServerFailLine1") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine2") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine3") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine4"),
                           UIHelper.GetString("btnIHaveCloseOtherPhoneHelper"),
                           UIHelper.GetString("btnExit"));
                });
                if (_continue)
                {
                    Thread.Sleep(12 * 1000);
                    AdbHelper.StartServer();
                    App.DevicesListener.Start();
                }
                else SystemHelper.AppExit(1);
            };
#if DEBUG
            AboutControl.LabelVersion.Content = SystemHelper.CurrentVersion + "-Debug";
            TitleBar.Title.Content += "  " + SystemHelper.CurrentVersion + "-Debug";
#else
            AboutControl.LabelVersion.Content = SystemHelper.CurrentVersion + "-Release";
            TitleBar.Title.Content += "  " + SystemHelper.CurrentVersion + "-Release";
#endif
        }
        /// <summary>
        /// 当设备监听器引发连接设备变化的事件时发生,可通过此事件获取最新的连接设备信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevicesChanged(object sender, DevicesChangedEventArgs e)
        {
            Logger.D("Devices change handing.....");
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
            UIHelper.SetOwnerTransparency(Config.BackgroundA);
            //开启Blur透明效果
            BlurHelper.EnableBlur(this);
            //刷新一下界面
            RefreshUI();
            //开始设备监听
            App.DevicesListener.Start();
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            if (Config.IsFirstLaunch)
            {
                MMessageBox.FastShow(App.OwnerWindow, FindResource("Notice2").ToString(), App.Current.Resources["msgFristLaunchNotice"].ToString());
                Config.IsFirstLaunch = false;
            }
            //开始获取公告
            GetNotice();
            //更新检测
            UpdateCheck();
        }
        /// <summary>
        /// 当窗口关闭时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //SystemHelper.AppExit(0);
        }
        /// <summary>
        /// 根据当前选中的设备刷新界面信息
        /// </summary>
        private void RefreshUI()
        {
            lock (setUILock)
            {
                PoweronFuncs.Refresh(App.SelectedDevice);
                RecoveryFuncs.Refresh(App.SelectedDevice);
                FastbootFuncs.Refresh(App.SelectedDevice);
                RebootGrid.Refresh(App.SelectedDevice);
                DevInfoPanel.Refresh(App.SelectedDevice);
            }
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
                App.SelectedDevice = ((DeviceBasicInfo)DevicesListBox.SelectedItem);
            }
            else if (this.DevicesListBox.SelectedIndex == -1)
            {
                App.SelectedDevice = new DeviceBasicInfo() { Status = DeviceStatus.None };
            }
            RefreshUI();
        }
        /// <summary>
        /// 获取公告
        /// </summary>
        void GetNotice()
        {
            new MOTDGetter().RunAsync((r) =>
            {
                textBoxGG.Dispatcher.Invoke(() =>
                {
                    textBoxGG.Text = r.Header + r.Separator + r.Message;
                });
            });
        }
        /// <summary>
        /// 更新检测
        /// </summary>
        void UpdateCheck()
        {
            new UpdateChecker().RunAsync((r) =>
            {
                if (r.NeedUpdate)
                {
                    new UpdateNoticeWindow(r).ShowDialog();
                }
            });
        }
    }
}
