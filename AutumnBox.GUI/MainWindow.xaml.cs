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
using AutumnBox.GUI.Cfg;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.UI;
using AutumnBox.GUI.UI.CstPanels;
using AutumnBox.GUI.Windows;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.Generic;
using AutumnBox.Support.CstmDebug;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Adb;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.MultipleDevices;

namespace AutumnBox.GUI
{
    [LogProperty(TAG = "Main Window")]
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window, ILogSender, IRefreshable
    {
        private Object setUILock = new System.Object();
        private List<IRefreshable> refreshables;
        public string LogTag => "Main Window";
        public bool IsShowLog => true;
        public MainWindow()
        {
            InitializeComponent();
            TitleBar.ImgMin.Visibility = Visibility.Visible;
            DevicesPanel.SelectionChanged += (s, e) =>
            {
                if (this.DevicesPanel.CurrentSelect.DevInfo.State == DeviceState.None)//如果没选择
                {
                    Reset();
                }
                else
                {
                    Refresh(this.DevicesPanel.CurrentSelect.DevInfo);
                }
            };
            DevInfoPanel.RefreshStart += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "DevInfoPanel refreshing");
                    //BoxHelper.ShowLoadingDialog();
                });
            };
            FunctionFlowBase.AnyFinished += FlowFinished;
            FunctionModule.AnyFinished += this.FuncFinish;
            DevInfoPanel.RefreshFinished += (s, e) =>
            {
                this.Dispatcher.Invoke(() =>
                {
                    Logger.D(this, "DevInfoPanel refreshed...");
                    //BoxHelper.CloseLoadingDialog();
                });
            };
            AdbHelper.AdbServerStartsFailed += (s, e) =>
            {
                DevicesMonitor.Stop();
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
                    Close();
                }
                Task.Run(() =>
                {
                    Thread.Sleep(3000);
                    App.Current.Dispatcher.Invoke(DevicesMonitor.Begin);
                });
            };
#if DEBUG
            TitleBar.Title.Content += "  " + SystemHelper.CurrentVersion + "-Debug";
#else
            TitleBar.Title.Content += "  " + SystemHelper.CurrentVersion + "-Release";
#endif
        }
        /// <summary>
        /// 各方面加载完毕,毫秒毫秒钟就要开始渲染了!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            refreshables = new List<IRefreshable>
            {
                this.RebootGrid,
                this.DevInfoPanel,
                this.FastbootFuncs,
                this.RecoveryFuncs,
                this.PoweronFuncs
            };
#if !DEBUG
            this.WTF.Navigate(Urls.STATISTICS_API);
#endif

#if ENABLE_BLUR
            UIHelper.SetOwnerTransparency(Config.BackgroundA);
            //开启Blur透明效果
            BlurHelper.EnableBlur(this);
#endif
            //刷新一下界面
            Reset();
            //开始获取公告
            new MOTDGetter().RunAsync((r) =>
            {
                textBoxGG.Text = r.Header + r.Separator + r.Message;
            });
            //检测更新
            new UpdateChecker().RunAsync((r) =>
            {
                Logger.D("need update?: " +  r.NeedUpdate);
                if (r.NeedUpdate) {
                    new UpdateNoticeWindow(r) { Owner = this }.ShowDialog();
                }
            });
            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            await Task.Run(() =>
            {
                Thread.Sleep(1000);
            });
            if (Config.IsFirstLaunch)
            {
                var aboutPanel = new FastPanel(this.GridMain, new About());
                aboutPanel.Closed += (s, _e) =>
                {
                    DevicesMonitor.Begin();
                    //更新检测
                    new UpdateChecker().RunAsync((r) =>
                    {
                        if (r.NeedUpdate)
                        {
                            new UpdateNoticeWindow(r).ShowDialog();
                        }
                    });
                };
                aboutPanel.Display();
            }
            else
            {
                //开始设备监听
                DevicesMonitor.Begin();
            }
        }
        public void Refresh(DeviceBasicInfo devinfo)
        {
            lock (setUILock)
            {
                refreshables.ForEach((ctrl) => { ctrl.Refresh(devinfo); });
            }
        }
        public void Reset()
        {
            lock (setUILock)
            {
                refreshables.ForEach((ctrl) => { ctrl.Reset(); });
            }
        }

        private void ButtonStartShell_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = AdbConstants.toolsPath,
                FileName = "cmd.exe",
                
            };
            if (SystemHelper.IsWin10)
            {
                var result = BoxHelper.ShowChoiceDialog("Notice", "msgShellChoiceTip", "Powershell", "CMD");
                switch (result)
                {
                    case ChoiceResult.BtnRight:
                        break;
                    case ChoiceResult.BtnLeft:
                        info.FileName = "powershell.exe";
                        break;
                    case ChoiceResult.BtnCancel:
                        return;
                }
            }
            Process.Start(info);
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new About()).Display();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new Settings()).Display();
        }

        private void BtnDonate_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new Donate()).Display();
        }

        private void TBHelp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(Urls.HELP_PAGE);
        }
        private void TBOfficialWebsite_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(Urls.OFFICIAL_WEBSITE);
        }

        private void TBLinkHelp_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Process.Start(Urls.LINK_HELP);
        }
    }
}
