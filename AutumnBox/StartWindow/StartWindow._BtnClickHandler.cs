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
using AutumnBox.Helper;
using AutumnBox.Windows;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox
{
    public partial class StartWindow
    {
        bool IsNightMode
        {
            get
            {
                return App.Current.Resources["ThemeName"].ToString() == "Night";
            }
        }
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
            if (!IsNightMode)
            {
                App.Current.Resources.MergedDictionaries[2].Source = new Uri($@"/AutumnBox.Res;component/Theme/NightTheme.xaml", UriKind.Relative);
            }
            else
            {
                App.Current.Resources.MergedDictionaries[2].Source = new Uri($@"/AutumnBox.Res;component/Theme/BasicTheme.xaml", UriKind.Relative);
            }
        }
    }
}
