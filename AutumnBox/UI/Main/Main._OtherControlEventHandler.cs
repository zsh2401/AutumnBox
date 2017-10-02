using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Functions;
using AutumnBox.Basic.Functions.RunningManager;
using AutumnBox.Debug;
using AutumnBox.Helper;
using AutumnBox.UI;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox
{
    /// <summary>
    /// 除基本的按钮点击逻辑外的UI逻辑
    /// </summary>
    public partial class Window1
    {
        private void CustomTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            UIHelper.DragMove(this, e);
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
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void LabelClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_normal)
            };
        }

        private void LabelClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_selected)
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
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.min_selected)
            };
        }

        private void LabelMin_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.min_normal)
            };
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

    }
}
