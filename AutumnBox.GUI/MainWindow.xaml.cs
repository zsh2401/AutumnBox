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
using AutumnBox.GUI.UI.Fp;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.GUI.UI.Cstm;
using System.Windows.Threading;
using AutumnBox.GUI.UI.FuncPanels;
using System.Windows.Controls;
using AutumnBox.GUI.I18N;
using System.Media;
using AutumnBox.OpenFramework.Management;
using System.Linq;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window, IDeviceRefreshable, ITitleBarWindow
    {
        private Object setUILock = new System.Object();
        private List<IExtPanel> extPanels;
        private List<IDeviceRefreshable> deviceRefreshables;
        private DeviceBasicInfo currentDevice;
        private SoundPlayer audioPlayer;
        public bool BtnMinEnable => true;

        Window ITitleBarWindow.MainWindow => this;

        public MainWindow()
        {
            InitializeComponent();
            audioPlayer = new SoundPlayer("Resources/Sound/ok.wav");
            deviceRefreshables = new List<IDeviceRefreshable>() {
                RebootGrid,
                DevInfoPanel,
            };
            extPanels = new List<IExtPanel>
            {
                FastbootFuncs,
                RecoveryFuncs,
                PoweronFuncs,
            };
            RegisterEvent();
            SetTitile();
            LanguageHelper.LanguageChanged += (s, e) =>
            {
                SetTitile();
                Reset();
            };
        }

        private void SetTitile()
        {
#if DEBUG
            TitleBar.Title = App.Current.Resources["AppName"] + "  " + SystemHelper.CurrentVersion.ToString(3) + "-Debug";
#else
            TitleBar.Title = App.Current.Resources["AppName"] + "  " + SystemHelper.CurrentVersion.ToString(3) + "-Release";
#endif
        }

        private void RegisterEvent()
        {
            DevicesPanel.SelectionChanged += (s, e) =>
            {
                if (this.DevicesPanel.CurrentSelectDevice.State == DeviceState.None)//如果没选择
                {
                    Reset();
                }
                else
                {
                    Refresh(this.DevicesPanel.CurrentSelectDevice);
                }
            };
            FunctionFlowBase.AnyFinished += FlowFinished;
            AdbHelper.AdbServerStartsFailed += (s, e) =>
            {
                DevicesMonitor.Stop();
                bool _continue = true;
                Dispatcher.Invoke(() =>
                {
                    _continue = BoxHelper.ShowChoiceDialog("msgWarning",
                        "msgStartAdbServerFail",
                        "btnExit", "btnIHaveCloseOtherPhoneHelper")
                        .ToBool();
                });
                if (!_continue)
                {
                    Close();
                }
                else
                {
                    Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        App.Current.Dispatcher.Invoke(DevicesMonitor.Begin);
                    });
                }
            };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs _e)
        {
#if !DEBUG
            Util.Extensions.SuppressScriptErrors(WTF, true);
            WTF.Navigate(App.Current.Resources["urlApiStatistics"].ToString());
#endif

#if ENABLE_BLUR
            UIHelper.SetOwnerTransparency(Config.BackgroundA);
            //开启Blur透明效果
            BlurHelper.EnableBlur(this);
            AllowsTransparency = true;
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
                if (r.NeedUpdate)
                {
                    new UpdateNoticeWindow(r) { Owner = this }.ShowDialog();
                }
            });

            //哦,如果是第一次启动本软件,那么就显示一下提示吧!
            Task.Run(() =>
            {
                Thread.Sleep(1000);
                Dispatcher.Invoke(() =>
                {
                    if (Properties.Settings.Default.IsFirstLaunch)
                    {
                        var aboutPanel = new FastPanel(this.GridMain, new AboutPanel());
                        aboutPanel.Display();
                    }
                });
            });
        }

        public void Refresh(DeviceBasicInfo devinfo)
        {
            lock (setUILock)
            {
                currentDevice = devinfo;
                extPanels.ForEach((ctrl) => { ctrl.Set(Manager.InternalManager.Warppers.ToArray(), devinfo); });
                deviceRefreshables.ForEach((ctrl) => { ctrl.Refresh(devinfo); });
                if (TBCFuncs.SelectedIndex == 4) return;
                switch (devinfo.State)
                {
                    case DeviceState.Poweron:
                        TBCFuncs.SelectedIndex = 1;
                        break;
                    case DeviceState.Recovery:
                    case DeviceState.Sideload:
                        TBCFuncs.SelectedIndex = 2;
                        break;
                    case DeviceState.Fastboot:
                        TBCFuncs.SelectedIndex = 3;
                        break;
                    default:
                        TBCFuncs.SelectedIndex = 4;
                        break;
                }
            };
        }

        public void Reset()
        {
            lock (setUILock)
            {
                Logger.Info(this,"reseting");
                extPanels.ForEach((ctrl) => { ctrl.Set(Manager.InternalManager.Warppers.ToArray()); });
                deviceRefreshables.ForEach((ctrl) => { ctrl.Reset(); });
                if (TBCFuncs.SelectedIndex == 4) return;
                TBCFuncs.SelectedIndex = 0;
            }
        }

        private void ButtonStartShell_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = AdbConstants.toolsPath,
                FileName = "cmd",
                UseShellExecute = false,
                Verb = "runas",
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
            new FastPanel(this.GridMain, new AboutPanel()).Display();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new SettingsPanel()).Display();
        }

        private void BtnDonate_Click(object sender, RoutedEventArgs e)
        {
            new FastPanel(this.GridMain, new DonatePanel()).Display();
        }

        private void _MainWindow_Closed(object sender, EventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                window.Close();
            }
        }

        public void OnBtnCloseClicked()
        {
            this.Close();
        }

        public void OnBtnMinClicked()
        {
            this.WindowState = WindowState.Minimized;
        }

        public void OnDragMove()
        {
            this.DragMove();
        }

        private void TBCFuncs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
