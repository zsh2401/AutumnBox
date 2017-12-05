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
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Function;
using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.UI.Cstm;
using AutumnBox.GUI.UI.Grids;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
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
    public sealed partial class MainWindow : Window, ILogSender
    {
        private Object setUILock = new System.Object();
        public string LogTag => "Main Window";
        public bool IsShowLog => true;
        public MainWindow()
        {
            InitializeComponent();
            App.DevicesListener.DevicesChanged += DevicesChanged;
            TitleBar.ImgMin.Visibility = Visibility.Visible;
            DevInfoPanel.RefreshStart += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "Refresh Start..");
                    Box.ShowLoadingDialog();
                });
            };
            FunctionFlowBase.AnyFinished += FlowFinished;
            FunctionModule.AnyFinished += this.FuncFinish;
            DevInfoPanel.RefreshFinished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "Refresh UI Finished..");
                    Box.CloseLoadingDialog();
                });
            };
            AdbHelper.AdbServerStartsFailed += (s, e) =>
            {
                App.DevicesListener.Cancel();
                bool _continue = true;
                Dispatcher.Invoke(() =>
                {
                    _continue = Box.BShowChoiceDialog("msgWarning",
                        UIHelper.GetString("msgStartAdbServerFailLine1") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine2") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine3") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine4"),
                        "btnIHaveCloseOtherPhoneHelper",
                        "btnExit"
                        );
                });
                if (_continue)
                {
                    Thread.Sleep(12 * 1000);
                    AdbHelper.StartServer();
                    App.DevicesListener.Begin();
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
#if ENABLE_BLUR
            BlurHelper.EnableBlur(this);
#endif
            //刷新一下界面
            RefreshUI();
            //开始设备监听
            App.DevicesListener.Begin();
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            if (Config.IsFirstLaunch)
            {
                BlockHelper.ShowMessageBlock("NoticeOnlyOne", "msgFristLaunchNotice");
                Config.IsFirstLaunch = false;
            }
            //开始获取公告
            new MOTDGetter().RunAsync((r) =>
            {
                textBoxGG.Text = r.Header + r.Separator + r.Message;
            });
            //更新检测
            new UpdateChecker().RunAsync((r) =>
            {
                if (r.NeedUpdate)
                {
                    new UpdateNoticeWindow(r).ShowDialog();
                }
            });
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
        private void ButtonLinkHelp_Click(object sender, RoutedEventArgs e)
        {
            new LinkHelpWindow().Show();
        }

        private  void ButtonStartShell_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = "adb/",
                FileName = "cmd.exe"
            };
            if (SystemHelper.IsWin10)
            {
                var result = Box.ShowChoiceDialog("Notice", "msgShellChoiceTip", "Powershell", "CMD");
                if (result == Windows.ChoiceResult.BtnLeft)
                {
                    info.FileName = "powershell.exe";
                }
                else if (result == Windows.ChoiceResult.BtnCancel)
                {
                    return;
                }
            }
            Process.Start(info);
        }

        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Urls.HELP_PAGE);
        }
    }
}
