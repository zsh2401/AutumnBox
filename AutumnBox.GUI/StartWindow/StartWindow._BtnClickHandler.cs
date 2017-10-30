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
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.NetUtil;
using AutumnBox.GUI.Windows;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox.GUI
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

        private void btnHelp_Click(object sender, RoutedEventArgs e) => Process.Start(Urls.HELP_PAGE);
    }
}
