/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 17:39:24 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AutumnBox.GUI.ViewModel
{
    class VMRunningWindow : ViewModelBase, IExtensionUIController
    {
        #region MVVM
        public FlexiableCommand Copy
        {
            get => _copy; set
            {
                _copy = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _copy;
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
        private readonly Window view;

        public event EventHandler<UIControllerClosingEventArgs> Closing;

        public VMRunningWindow(Window view)
        {
            this.view = view;
            //this.wrapper = wrapper;
            //Title = wrapper.Info.Name;
            //Icon = wrapper.Info.Icon.ToExtensionIcon();
            Copy = new FlexiableCommand(() =>
            {
                Clipboard.SetText(Output);
            });
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
            var args = new UIControllerClosingEventArgs();
            Closing?.Invoke(this, args);
            return args.Cancel;
        }

        //public void Stop()
        //{
        //    bool stopped = wrapper.Stop();
        //    if (stopped)
        //    {
        //        AppendLine(App.Current.Resources["RunningWindowStopped"].ToString());
        //        Tip = "被强制终止";
        //        ProgressValue = 100;
        //        wrapperIsRunning = false;
        //    }
        //    else
        //    {
        //        AppendLine(App.Current.Resources["RunningWindowCantStop"].ToString());
        //    }
        //}

        public void OnStart(IExtInfoGetter info)
        {
            view.Dispatcher.Invoke(() =>
            {
                Title = info.Name;
                Icon = info.Icon.ToExtensionIcon();
                ProgressValue = -1;
                //Tip = "RunningWindowStateRunning";
                view.Show();
            });
        }

        public void OnFinish()
        {
            ProgressValue = 100;
            //wrapperIsRunning = false;
            //view.Dispatcher.Invoke(() =>
            //{

            //    Tip = args.ReturnCode == 0 ? "RunningWindowStateFinished" : "RunningWindowStateError";
            //});
        }
    }
}
