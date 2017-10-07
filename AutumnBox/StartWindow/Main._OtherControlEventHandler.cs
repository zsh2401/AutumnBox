/* =============================================================================*\
*
* Filename: Main._OtherControlEventHandler.cs
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
using AutumnBox.Helper;
using AutumnBox.UI;
using AutumnBox.Windows;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox
{
    /// <summary>
    /// 除基本的按钮点击逻辑外的UI逻辑
    /// </summary>
    public partial class StartWindow
    {
        private void GridBuildInfo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) => RefreshUI();

        private void TextBlockGoToOS_MouseDown(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkAutumnBoxOS"].ToString());

        private void TextBlockDonate_MouseDown(object sender, MouseButtonEventArgs e) =>
            new DonateWindow(this).ShowDialog();

        private void CustomTitleBar_MouseMove(object sender, MouseEventArgs e) =>
            UIHelper.DragMove(this, e);

        private void MenuItem_Click(object sender, RoutedEventArgs e) =>
            new DonateWindow(this).ShowDialog();

        private void LabelClose_MouseLeave(object sender, MouseEventArgs e) =>
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_normal)
            };

        private void LabelClose_MouseEnter(object sender, MouseEventArgs e) =>
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_selected)
            };

        private void LabelMin_MouseDown(object sender, MouseButtonEventArgs e) =>
            this.WindowState = WindowState.Minimized;

        private void LabelMin_MouseEnter(object sender, MouseEventArgs e) =>
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.min_selected)
            };

        private void LabelMin_MouseLeave(object sender, MouseEventArgs e) =>
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.min_normal)
            };
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e) =>
            Process.Start("explorer.exe", App.Current.Resources["linkAICP"].ToString());

        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkLineageOS"].ToString());

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkMokee"].ToString());

        private void TextBlock_MouseDown_3(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkMiFlashToolBox"].ToString());

        private void TextBlock_MouseDown_4(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkMiLineFlashPackage"].ToString());

        private void TextBlock_MouseDown_5(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkDownloadMIUI"].ToString());

        private void TextBlock_MouseDown_6(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkDownloadTwrp"].ToString());

        private void TextBlock_MouseDown_7(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkOpenGAPPS"].ToString());

        private void TextBlock_MouseDown_8(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkAutumnBoxOS"].ToString());

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }
    }
}
