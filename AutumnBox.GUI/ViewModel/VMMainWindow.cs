/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 19:04:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.Windows;
using MaterialDesignThemes.Wpf;
using System.Diagnostics;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindow : ViewModelBase
    {
        public ICommand StartShell => new MVVMCommand((p)=> {

            ProcessStartInfo info = new ProcessStartInfo
            {
                WorkingDirectory = AdbConstants.toolsPath,
                FileName = "cmd",
                UseShellExecute = false,
                Verb = "runas",
            };
            var args = new ChoicerContentStartArgs
            {
                Content = "msgShellChoiceTip",
                ContentCenterButton = "CMD",
                ContentRightButton = "PowerShell"
            };
            args.Choiced += (s, e) =>
            {
                switch (e.Result)
                {
                    case ChoicerResult.Center:
                        Process.Start(info);
                        break;
                    case ChoicerResult.Right:
                        info.FileName = "powershell.exe";
                        Process.Start(info);
                        break;
                    default:
                        break;
                }
            };
            MaterialDialog.ShowChoiceDialog(args);
        });
        public ICommand ShowSettingsDialog => new MVVMCommand((args) =>
        {
            DialogHost.Show(new ContentSettings());
        });
        public ICommand ShowDonateDialog => new MVVMCommand((args) =>
        {
            DialogHost.Show(new ContentDonate());
        });
    }
}
