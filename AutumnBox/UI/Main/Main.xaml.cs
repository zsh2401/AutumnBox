using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Debug;
using AutumnBox.UI;
using AutumnBox.Util;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            Log.InitLogFile();
            Log.d(TAG, "Log Init Finish,Start Init Window");
            InitializeComponent();
            //buttonChangeTheme.click
            CustomInit();
        }

        private void CustomTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void LabelClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(Res.DynamicIcons.close_normal)
            };
        }

        private void LabelClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(Res.DynamicIcons.close_selected)
            };
        }

        private void LabelMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LabelMin_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(Res.DynamicIcons.min_selected)
            };
        }

        private void LabelMin_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(Res.DynamicIcons.min_normal)
            };
        }

        private void DevicesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.DevicesListBox.SelectedIndex != -1)//如果选择了设备
            {
                new Thread(() =>
                {
                    SetUIByDevices();
                }).Start();
                ShowRateBox();
            }
            else
            {
                this.AndroidVersionLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                this.CodeLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                this.ModelLabel.Content = Application.Current.FindResource("PleaseSelectedADevice").ToString();
                ChangeButtonAndImageByStatus(DeviceStatus.NO_DEVICE);
            }
        }

        private void buttonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = "选择一个文件";
            fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                FileSender fs = new FileSender(new FileArgs { files = new string[] { fileDialog.FileName } });
                RunningManager rm = App.nowLink.GetRunningManager(fs);
                rm.FuncEvents.Finished += FuncFinish;
                rm.FuncStart();
                //ShowRateBox(rm);
                new FileSendingWindow(this, rm).ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
            App.devicesListener.Stop();
            Basic.Util.Tools.KillAdb();
            Environment.Exit(0);
        }

        private void buttonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.Recovery,
                nowStatus = App.nowLink.DeviceInfo.deviceStatus
            });
            var rm = App.nowLink.GetRunningManager(ro);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
        }

        private void buttonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.Bootloader,
                nowStatus = App.nowLink.DeviceInfo.deviceStatus
            });
            var rm = App.nowLink.GetRunningManager(ro);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
        }

        private void buttonRebootToSystem_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.System,
                nowStatus = App.nowLink.DeviceInfo.deviceStatus
            });
            var rm = App.nowLink.GetRunningManager(ro);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
        }

        private void buttonFlashCustomRecovery_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = "选择一个文件";
            fileDialog.Filter = "镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                CustomRecoveryFlasher flasher = new CustomRecoveryFlasher(new FileArgs() { files = new string[] { fileDialog.FileName } });
                var rm = App.nowLink.GetRunningManager(flasher);
                rm.FuncEvents.Finished += FuncFinish;
                rm.FuncStart();
                ShowRateBox(rm);
            }
            else
            {
                return;
            }
        }

        private void buttonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, FindResource("Notice").ToString(), FindResource("UnlockXiaomiSystemTip").ToString())) return;
            MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("IfAllOK").ToString());
            XiaomiSystemUnlocker unlocker = new XiaomiSystemUnlocker();
            var rm = App.nowLink.GetRunningManager(unlocker);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
            ShowRateBox(rm);
        }

        private void buttonRelockMi_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, FindResource("Warning").ToString(), FindResource("RelockWarning").ToString())) return;
            if (!ChoiceBox.Show(this, FindResource("Warning").ToString(), FindResource("RelockWarningAgain").ToString())) return;
            XiaomiBootloaderRelocker relocker = new XiaomiBootloaderRelocker();
            var rm = App.nowLink.GetRunningManager(relocker);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
            ShowRateBox(rm);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new DonateWindow(this).ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            new AboutWindow(this).ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            new ContactWindow(this).ShowDialog();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "http://dwnld.aicp-rom.com/");
        }

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://www.lineageos.org/");
        }

        //private void buttonSideload_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DevicesTools.GetDeviceStatus(DevicesListBox.SelectedItem.ToString()) != DeviceStatus.SIDELOAD)
        //    {
        //        MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("SideloadTur").ToString());
        //        return;
        //    }
        //    if (!ChoiceBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("SideloadTip").ToString())) return;
        //    OpenFileDialog fd = new OpenFileDialog();
        //    fd.Multiselect = false;
        //    fd.Filter = "刷机包文件|*.zip";
        //    if (fd.ShowDialog() == true)
        //    {
        //        core.Sideload(new string[] { DevicesListBox.SelectedItem.ToString(), fd.FileName });
        //    }
        //}

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://download.mokeedev.com/");
        }

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "http://www.miui.com/thread-6685031-1-1.html");
        }

        private void TextBlock_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "http://www.miui.com/shuaji-393.html");
        }

        private void TextBlock_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "http://www.miui.com/download.html");
        }

        private void TextBlock_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://twrp.me/Devices/");
        }

        private void TextBlock_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "http://opengapps.org/");
        }

        private void TextBlock_MouseDown_8(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/zsh2401/AutumnBox");
        }

        private void MainWindow_Initialized(object sender, EventArgs e)
        {
            Log.d(TAG, "Init Window Finish");
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
            if (Config.isFristLaunch)
            {
                MMessageBox.ShowDialog(this, FindResource("Notice2").ToString(), FindResource("FristLaunchNotice").ToString());
                Config.isFristLaunch = false;
            }
        }

        private void buttonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, FindResource("Notice").ToString(), FindResource("StartBrventTip").ToString())) return;
            BreventServiceActivator activator = new BreventServiceActivator();
            var rm = App.nowLink.GetRunningManager(activator);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
            ShowRateBox(rm);
        }

        private void buttonLinkHelp_Click(object sender, RoutedEventArgs e)
        {
            new LinkHelpWindow(this).Show();
            //Application.Current.Resources.MergedDictionaries[1] = new ResourceDictionary()
            //{
            //    Source = new Uri("UI/Color2.xaml", UriKind.Relative)
            //};
        }

        private void buttonStartShell_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.WorkingDirectory = "adb/";
            info.FileName = "cmd.exe";
            if (Tools.IsWin10)
            {
                if (ChoiceBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("ShellChoiceTip").ToString(), "Powershell", "CMD"))
                {
                    info.FileName = "powershell.exe";
                }
            }
            Process.Start(info);
        }

        private void buttonChangeTheme_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
