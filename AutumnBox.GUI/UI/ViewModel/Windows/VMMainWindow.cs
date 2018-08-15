/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 19:04:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.UI.View.DialogContent;
using AutumnBox.GUI.Windows;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.ViewModel.Windows
{
    class VMMainWindow : ViewModelBase
    {
        public ICommand StartShell => new MVVMCommand((args)=> {
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
        });
        public ICommand ShowSettingsDialog => new MVVMCommand((args) =>
        {
            DialogHost.Show(new ContentSettings());
        });
        public ICommand ShowAboutDialog => new MVVMCommand((args) =>
        {
            DialogHost.Show(new ContentAbout());
        });
        public ICommand ShowDonateDialog => new MVVMCommand((args) =>
        {
            DialogHost.Show(new ContentDonate());
        });
    }
}
