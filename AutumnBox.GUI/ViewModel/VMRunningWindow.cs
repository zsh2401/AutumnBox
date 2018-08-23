/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 17:39:24 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Warpper;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox.GUI.ViewModel
{
    class VMRunningWindow : ViewModelBase, IExtensionUIController
    {
        #region MVVM
        public double ProgressValue
        {
            get => progressValue; set
            {
                if (value == -1)
                {
                    IsIndeterminate = true;
                    progressValue = 0;
                    return;
                }
                IsIndeterminate = false;
                progressValue = value;
                RaisePropertyChanged();
            }
        }
        private double progressValue;

        public ImageSource Icon
        {
            get => _icon; set
            {
                _icon = value;
                RaisePropertyChanged();
            }
        }
        private ImageSource _icon;

        public bool IsIndeterminate
        {
            get => _isIndeterminate; set
            {
                _isIndeterminate = value;
                RaisePropertyChanged();
            }
        }
        private bool _isIndeterminate;

        public string Tip
        {
            get => tip; set
            {
                tip = App.Current.Resources[value]?.ToString() ?? value;
                RaisePropertyChanged();
            }
        }
        private string tip;

        public string Title
        {
            get => _title; set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        public string Output
        {
            get => output; set
            {
                output = value;
                RaisePropertyChanged();
            }
        }
        private string output;

        #endregion

        private bool warpperIsRunning = false;
        private readonly IExtensionWarpper warpper;
        private readonly Window view;

        public VMRunningWindow(Window view, IExtensionWarpper warpper)
        {
            this.view = view;
            this.warpper = warpper;
            Title = warpper.Info.Name;
            Icon = warpper.Info.Icon.ToExtensionIcon();
            output = "";
        }

        public void AppendLine(string msg)
        {
            view.Dispatcher.Invoke(() =>
            {
                output += msg + Environment.NewLine;
                RaisePropertyChanged(nameof(Output));
            });
        }

        public bool OnWindowClosing()
        {
            if (warpperIsRunning)
            {
                Task.Run(() =>
                {
                    Stop();
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Stop()
        {
            bool stopped = warpper.Stop();
            if (stopped)
            {
                AppendLine("已停止...");
                Tip = "被强制终止";
                ProgressValue = 100;
                warpperIsRunning = false;
            }
            else
            {
                AppendLine("无法被停止");
            }
        }

        public void OnStart()
        {
            view.Dispatcher.Invoke(() =>
            {
                ProgressValue = -1;
                Tip = "RunningWindowStateRunning";
                warpperIsRunning = true;
                view.Show();
            });
        }

        public void OnFinish(int exitCode, bool isForceStopped)
        {
            view.Dispatcher.Invoke(() =>
            {
                warpperIsRunning = false;
                ProgressValue = 100;
                Tip = exitCode == 0 ? "RunningWindowStateFinished" : "RunningWindowStateError";
            });
        }
    }
}
