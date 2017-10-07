/* =============================================================================*\
*
* Filename: Main.FuncBtnClickHandler.cs
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
using AutumnBox.Basic.Function;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Modules;
using AutumnBox.Basic.Function.RunningManager;
using AutumnBox.Helper;
using AutumnBox.Windows;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox
{
    public partial class StartWindow
    {
        private void ButtonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgStartBrventTip"].ToString())) return;
            BreventServiceActivator activator = new BreventServiceActivator();
            var rm = App.SelectedDevice.GetRunningManger(activator);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
            UIHelper.ShowRateBox(this,rm);
        }

        private void ButtonLinkHelp_Click(object sender, RoutedEventArgs e)
        {
            new LinkHelpWindow(this).Show();
        }

        private void ButtonStartShell_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = "adb/",
                FileName = "cmd.exe"
            };
            if (SystemHelper.IsWin10)
            {
                if (ChoiceBox.ShowDialog(this, App.Current.Resources["Notice"].ToString(), App.Current.Resources["msgShellChoiceTip"].ToString(), "Powershell", "CMD"))
                {
                    info.FileName = "powershell.exe";
                }
            }
            Process.Start(info);
        }

        private void ButtonChangeTheme_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }

        private void ButtonPushFileToSdcard_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = App.Current.Resources["SelecteAFile"].ToString();
            fileDialog.Filter = "刷机包/压缩包文件(*.zip)|*.zip|镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                FileSender fs = new FileSender(new FileArgs { files = new string[] { fileDialog.FileName } });
                RunningManager rm = App.SelectedDevice.GetRunningManger(fs);
                rm.FuncEvents.Finished += FuncFinish;
                rm.FuncStart();
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

        private void ButtonRebootToRecovery_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.Recovery,
                nowStatus = App.SelectedDevice.Status
            });
            var rm = App.SelectedDevice.GetRunningManger(ro);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
        }

        private void ButtonRebootToBootloader_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.Bootloader,
                nowStatus = App.SelectedDevice.Status
            });
            var rm = App.SelectedDevice.GetRunningManger(ro);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
        }

        private void ButtonRebootToSystem_Click(object sender, RoutedEventArgs e)
        {
            RebootOperator ro = new RebootOperator(new RebootArgs
            {
                rebootOption = RebootOptions.System,
                nowStatus = App.SelectedDevice.Status
            });
            var rm = App.SelectedDevice.GetRunningManger(ro);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
        }

        private void ButtonFlashCustomRecovery_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Reset();
            fileDialog.Title = "选择一个文件";
            fileDialog.Filter = "镜像文件(*.img)|*.img|全部文件(*.*)|*.*";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() == true)
            {
                CustomRecoveryFlasher flasher = new CustomRecoveryFlasher(new FileArgs() { files = new string[] { fileDialog.FileName } });
                var rm = App.SelectedDevice.GetRunningManger(flasher);
                rm.FuncEvents.Finished += FuncFinish;
                rm.FuncStart();
                UIHelper.ShowRateBox(this,rm);
            }
            else
            {
                return;
            }
        }

        private void ButtonUnlockMiSystem_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, FindResource("Notice").ToString(), FindResource("msgUnlockXiaomiSystemTip").ToString())) return;
            MMessageBox.ShowDialog(this, FindResource("Notice").ToString(), FindResource("msgIfAllOK").ToString());
            XiaomiSystemUnlocker unlocker = new XiaomiSystemUnlocker();
            var rm = App.SelectedDevice.GetRunningManger(unlocker);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
            UIHelper.ShowRateBox(this,rm);
        }

        private void ButtonRelockMi_Click(object sender, RoutedEventArgs e)
        {
            if (!ChoiceBox.Show(this, App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgRelockWarning"].ToString())) return;
            if (!ChoiceBox.Show(this, App.Current.Resources["Warning"].ToString(), App.Current.Resources["msgRelockWarningAgain"].ToString())) return;
            XiaomiBootloaderRelocker relocker = new XiaomiBootloaderRelocker();
            var rm = App.SelectedDevice.GetRunningManger(relocker);
            rm.FuncEvents.Finished += FuncFinish;
            rm.FuncStart();
            UIHelper.ShowRateBox(this,rm);
        }
    }
}
