/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 19:08:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.DialogContent;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMPanelMain : ViewModelBase
    {
        public ICommand StartShell => new MVVMCommand((p) =>
        {

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
            View.MaterialDialog.ShowChoiceDialog(args);
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
