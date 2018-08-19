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
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AutumnBox.Basic.Util;
using AutumnBox.Basic.MultipleDevices;
using System.Windows.Threading;
using System.Media;
using AutumnBox.GUI.ViewModel;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.Util;

namespace AutumnBox.GUI
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private readonly SoundPlayer audioPlayer = new SoundPlayer("Resources/Sound/ok.wav");
        private readonly VMMainWindow ViewModel = new VMMainWindow();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private void RegisterEvent()
        {
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
            ViewModel.LoadAsync(() =>
            {
                RegisterEvent();
            });
#if !DEBUG
            Util.Extensions.SuppressScriptErrors(WTF, true);
            WTF.Navigate(App.Current.Resources["urlApiStatistics"].ToString());
#endif
            //#if ENABLE_BLUR
            //            UIHelper.SetOwnerTransparency(Config.BackgroundA);
            //            //开启Blur透明效果
            //            BlurHelper.EnableBlur(this);
            //            AllowsTransparency = true;
            //#endif
            //            new UpdateChecker().RunAsync((r) =>
            //            {
            //                if (r.NeedUpdate)
            //                {
            //                    new UpdateNoticeWindow(r) { Owner = this }.ShowDialog();
            //                }
            //            });
        }

        private void _MainWindow_Closed(object sender, EventArgs e)
        {
            foreach (Window window in App.Current.Windows)
            {
                window.Close();
            }
        }
    }
}
