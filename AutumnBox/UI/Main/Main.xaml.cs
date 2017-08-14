#define LANGUAGE_TEST
using AutumnBox.Basic;
using AutumnBox.Basic.Devices;
using AutumnBox.Debug;
using AutumnBox.Images.DynamicIcons;
using AutumnBox.UI;
using AutumnBox.Util;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
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
        Core core = new Core();
        RateBox rateBox;
        string TAG = "MainWindow";
        public Window1()
        {

            Log.InitLogFile();
            Log.d(TAG, "Log Init Finish,Start Init Window");
            //#if LANGUAGE_TEST
            //            ChangeLanguage("en-us");
            //#else
            //            if (Config.language == "not_set")
            //            {
            //                if (
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x0C04 &&//如果当前系统语言为中文（香港特别行政区，中国）
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x1404 &&//..中文（澳门特别行政区） 
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x0804 &&//..中文（中国） 
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x0004 &&//..中文（简体） 
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x1004 &&//..中文（新加坡） 
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x0404 &&//..中文（台湾） 
            //                Thread.CurrentThread.CurrentCulture.LCID == 0x7C04//..中文（繁体） 
            //                )
            //                {
            //                    ChangeLanguage("zh-cn");
            //                    Config.language = "zh-cn";
            //                }
            //                else
            //                {
            //                    ChangeLanguage("en-us");
            //                    Config.language = "en-us";
            //                }
            //            }
            //            else {
            //                if (Config.language != "zh-cn") {
            //                    ChangeLanguage(Config.language);
            //                }
            //            }
            //#endif
            InitializeComponent();

            InitEvents();//绑定各种事件
            ChangeButtonAndImageByStatus(DeviceStatus.NO_DEVICE);//将所有按钮设置成关闭状态
            core.devicesListener.Start();//开始设备监听
            Log.d(TAG, "Init Window Finish");
            string szTmp = "http://miui.com";
            Uri uri = new Uri(szTmp);
            //Web.Navigate(uri);

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
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.close_normal)
            };
        }

        private void LabelClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.close_selected)
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
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.min_selected)
            };
        }

        private void LabelMin_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.min_normal)
            };
        }

        private void DevicesListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this.DevicesListBox.SelectedIndex != -1)
            {
                new Thread(new ParameterizedThreadStart(SetUIByDevices)).Start(this.DevicesListBox.SelectedItem.ToString());
                rateBox = new RateBox(this);
                rateBox.ShowDialog();
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
                Thread t = new Thread(new ParameterizedThreadStart(core.PushFileToSdcard));
                string[] args = { this.DevicesListBox.SelectedItem.ToString(), fileDialog.FileName };
                t.Start(args);
                this.rateBox = new RateBox(this);
                this.rateBox.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void buttonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            core.Reboot(DevicesListBox.SelectedItem.ToString(), Basic.Other.RebootOptions.Recovery);
        }

        private void buttonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            core.Reboot(DevicesListBox.SelectedItem.ToString(), Basic.Other.RebootOptions.Bootloader);
        }

        private void buttonRebootToSystem_Click(object sender, RoutedEventArgs e)
        {
            core.Reboot(DevicesListBox.SelectedItem.ToString(), Basic.Other.RebootOptions.System);
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
                Thread t = new Thread(new ParameterizedThreadStart(core.FlashCustomRecovery));
                string[] args = { this.DevicesListBox.SelectedItem.ToString(), fileDialog.FileName };
                t.Start(args);
                this.rateBox = new RateBox(this);
                this.rateBox.ShowDialog();
            }
            else
            {
                return;
            }
        }

        private void buttonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            new Thread(
                new ParameterizedThreadStart(core.UnlockMiSystem)).Start(this.DevicesListBox.SelectedItem);
            this.rateBox = new RateBox(this);
            rateBox.ShowDialog();
        }

        private void buttonRelockMi_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, TryFindResource("Warning").ToString(), FindResource("RelockWarning").ToString())) return;
            if (!ChoiceBox.Show(this, TryFindResource("Warning").ToString(), FindResource("RelockWarningAgain").ToString())) return;
            new Thread(
                new ParameterizedThreadStart(core.RelockMi)).Start(this.DevicesListBox.SelectedItem);
            this.rateBox = new RateBox(this);
            rateBox.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            new DonateWindow(this).ShowDialog();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //MMessageBox.ShowDialog(this, FindResource("About").ToString(), FindResource("AboutMessage").ToString());
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

        private void buttonSideload_Click(object sender, RoutedEventArgs e)
        {
            if (core.GetDeviceStatus(DevicesListBox.SelectedItem.ToString()) != DeviceStatus.SIDELOAD)
            {
                MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("SideloadTur").ToString());
                return;
            }
            if (!ChoiceBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("SideloadTip").ToString())) return;
            OpenFileDialog fd = new OpenFileDialog();
            fd.Multiselect = false;
            fd.Filter = "刷机包文件|*.zip";
            if (fd.ShowDialog() == true)
            {
                core.Sideload(new string[] { DevicesListBox.SelectedItem.ToString(), fd.FileName });
            }
        }
    }
}
