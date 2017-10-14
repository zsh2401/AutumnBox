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
        private void ButtonLinkHelp_Click(object sender, RoutedEventArgs e)
        {
            new LinkHelpWindow().Show();
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
            new ChangeThemeWindow().Show();
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
    }
}
