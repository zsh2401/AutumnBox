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
using AutumnBox.GUI.UI;
using AutumnBox.GUI.UI.Cstm;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.UI.Grids;
using AutumnBox.GUI.Windows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections;
using System.Collections.Generic;
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Util;

namespace AutumnBox.GUI
{
    [LogProperty(TAG = "Main Window")]
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window, ILogSender
    {
        private Object setUILock = new System.Object();
        private List<IRefreshable> refreshables;
        public string LogTag => "Main Window";
        public bool IsShowLog => true;
        public MainWindow()
        {
            InitializeComponent();
            App.StaticProperty.DevicesListener.DevicesChanged += DevicesChanged;
            TitleBar.ImgMin.Visibility = Visibility.Visible;
            DevInfoPanel.RefreshStart += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "Refresh Start..");
                    BoxHelper.ShowLoadingDialog();
                });
            };
            FunctionFlowBase.AnyFinished += FlowFinished;
            FunctionModule.AnyFinished += this.FuncFinish;
            DevInfoPanel.RefreshFinished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "Refresh UI Finished..");
                    BoxHelper.CloseLoadingDialog();
                });
            };
            AdbHelper.AdbServerStartsFailed += (s, e) =>
            {
                App.StaticProperty.DevicesListener.Cancel();
                bool _continue = true;
                Dispatcher.Invoke(() =>
                {
                    _continue = BoxHelper.ShowChoiceDialog("msgWarning",
                        UIHelper.GetString("msgStartAdbServerFailLine1") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine2") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine3") + Environment.NewLine +
                           UIHelper.GetString("msgStartAdbServerFailLine4"),
                        "btnExit",
                        "btnIHaveCloseOtherPhoneHelper"
                        ).ToBool();
                });
                if (!_continue)
                {
                    SystemHelper.AppExit(1);
                }
                Task.Run(()=> {
                    Thread.Sleep(3000);
                    App.Current.Dispatcher.Invoke(App.StaticProperty.DevicesListener.Begin);
                });
                
            };
#if DEBUG
            TitleBar.Title.Content += "  " + SystemHelper.CurrentVersion + "-Debug";
#else
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

            //refreshables = new FieldsFinder<MainWindow,IRefreshable>().FindFrom(this).ToList();

            refreshables = new List<IRefreshable>
            {
                this.RebootGrid,
                this.DevInfoPanel,
                this.FastbootFuncs,
                this.RecoveryFuncs,
                this.PoweronFuncs
            };
#if ENABLE_BLUR
            UIHelper.SetOwnerTransparency(Config.BackgroundA);
            //开启Blur透明效果
            BlurHelper.EnableBlur(this);
#endif
            //刷新一下界面
            Refresh();
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                this.Dispatcher.Invoke(() =>
                {
                    if (Config.IsFirstLaunch)
                    {
                        new FastGrid(this.GridMain, new About(), () => { App.StaticProperty.DevicesListener.Begin(); });
                        Config.IsFirstLaunch = false;
                    }
                    else
                    {
                        //开始设备监听
                        App.StaticProperty.DevicesListener.Begin();
                    }
                });
            });
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
        public void Refresh()
        {
            refreshables.ForEach((ctrl)=> { ctrl.Refresh(App.StaticProperty.DeviceConnection.DevInfo); });
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
                App.StaticProperty.DeviceConnection.Reset((DeviceBasicInfo)DevicesListBox.SelectedItem);
            }
            else if (this.DevicesListBox.SelectedIndex == -1)
            {
                App.StaticProperty.DeviceConnection.Reset();
            }
            Refresh();
        }

        private void ButtonStartShell_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = ConstData.toolsPath,
                FileName = "cmd.exe"
            };
            if (SystemHelper.IsWin10)
            {
                var result = BoxHelper.ShowChoiceDialog("Notice", "msgShellChoiceTip", "Powershell", "CMD");
                if (result == ChoiceResult.BtnLeft)
                {
                    info.FileName = "powershell.exe";
                }
                else if (result == ChoiceResult.BtnCancel)
                {
                    return;
                }
            }
            Process.Start(info);
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            new FastGrid(this.GridMain, new About());
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            new FastGrid(this.GridMain, new Settings());
        }

        private void TBHelp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(Urls.HELP_PAGE);
        }

        private void BtnDonate_Click(object sender, RoutedEventArgs e)
        {
            new FastGrid(this.GridMain, new Donate());
        }

        private void BtnLinkHelp_Click(object sender, RoutedEventArgs e)
        {
            new LinkHelpWindow().Show();
        }
    }
}
