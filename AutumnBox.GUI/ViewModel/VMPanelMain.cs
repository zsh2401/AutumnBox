/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 19:08:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.GUI.View.Windows;
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
        public ICommand StartShell { get; private set; }
        public ICommand ShowSettingsDialog { get; private set; }

        public int TabSelectedIndex
        {
            get => tabSelectedIndex;
            set
            {
                tabSelectedIndex = value;
                RaisePropertyChanged();
            }
        }
        private int tabSelectedIndex;
        public VMPanelMain()
        {
            StartShell = new FlexiableCommand(_StartShell);
            ShowSettingsDialog = new FlexiableCommand(_ShowSettingsDialog);
            Util.Bus.DeviceSelectionObserver.Instance.SelectedNoDevice += NoDevice;
            Util.Bus.DeviceSelectionObserver.Instance.SelectedDevice += Instance_SelectedDevice;
        }
        private void _ShowSettingsDialog()
        {
            //new MessageWindow().ShowDialog();
            DialogHost.Show(new ContentSettings());
        }
        private void _StartShell()
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
        }
        private void Instance_SelectedDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 1;
        }

        private void NoDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 0;
        }
    }
}
