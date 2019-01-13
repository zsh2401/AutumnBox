using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.UI;
using AutumnBox.OpenFramework.Extension.LeafExtension;
using System;
using System.Diagnostics;
using System.Windows;

namespace AutumnBox.GUI.ViewModel
{
    class VMLeafUI : ViewModelBase, ILeafUI
    {
        public FlexiableCommand Copy
        {
            get => _copy; set
            {
                _copy = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _copy;

        public bool IsIndeterminate
        {
            get => _isIndeterminate; set
            {
                _isIndeterminate = value;
                RaisePropertyChanged();
            }
        }
        private bool _isIndeterminate;

        public Window View
        {
            get => _view; set
            {
                _view = value;
                InitView();
                RaisePropertyChanged();
            }
        }
        private Window _view;

        private void InitView()
        {
            View.Closing += View_Closing;
        }

        private void View_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_isFinished) return;
            LeafCloseBtnClickedEventArgs args = new LeafCloseBtnClickedEventArgs
            {
                CanBeClosed = false
            };
            CloseButtonClicked?.Invoke(this, args);
            e.Cancel = !args.CanBeClosed;
            if (args.CanBeClosed) Lock();
            else WriteLine(App.Current.Resources["RunningWindowCantStop"]);
        }

        private bool _locked;
        public void Lock()
        {
            _locked = true;
        }

        public VMLeafUI()
        {
            RaisePropertyChangedOnDispatcher = true;
            Title = "LeafUI Window";
            Progress = -1;
            Tip = App.Current.Resources["RunningWindowStateRunning"] as string;
            Icon = null;
            Copy = new FlexiableCommand(() =>
            {
                try
                {
                    Clipboard.SetText(Content);
                }
                catch { }
            });
        }

        public string Content
        {
            get => _content; set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
        private string _content;

        public double Progress
        {
            get => _progress; set
            {
                if (_locked) return;
                if (value == -1)
                {
                    IsIndeterminate = true;
                    _progress = 0;
                    RaisePropertyChanged();
                    return;
                }
                IsIndeterminate = false;
                _progress = value;
                RaisePropertyChanged();
            }
        }
        private double _progress;


        public string Tip
        {
            get => _tip; set
            {
                if (_locked) return;
                _tip = value;
                RaisePropertyChanged();
            }
        }
        private string _tip;

        public System.Drawing.Size Size
        {
            get
            {
                System.Drawing.Size size = new System.Drawing.Size();
                View.Dispatcher.Invoke(() =>
                {
                    size.Height = (int)View.Height;
                    size.Width = (int)View.Width;
                });
                return size;
            }
            set
            {
                if (_locked) return;
                View.Dispatcher.Invoke(() =>
                {
                    View.Height = value.Height;
                    View.Width = value.Width;
                });
            }
        }

        public byte[] Icon
        {
            get => _icon; set
            {
                if (_locked) return;
                _icon = value;
                RaisePropertyChanged();
            }
        }
        private byte[] _icon;

        public string Title
        {
            get => _title; set
            {
                if (_locked) return;
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        /// <summary>
        /// 点击关闭按钮时发生
        /// </summary>
        public event EventHandler<LeafCloseBtnClickedEventArgs> CloseButtonClicked;

        /// <summary>
        /// 是否已经完成
        /// </summary>
        private bool _isFinished = false;

        public void EnableHelpBtn(Action callback)
        {
            View.Dispatcher.Invoke(() =>
            {
                HelpButtonHelper.EnableHelpButton(View, callback);
            });
        }

        public void Finish(int exitCode = 0)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Trace.WriteLine("LeafUITipCode" + exitCode);
                Finish(App.Current.Resources["LeafUITipCode" + exitCode] as string
                    ?? App.Current.Resources["LeafUITipCodeUnknown"] as string);
            });
        }

        public void Finish(string tip)
        {
            if (_locked) return;
            Tip = tip;
            Progress = 100;
            Lock();
            _isFinished = true;
        }

        public void Show()
        {
            if (_locked) return;
            View.Dispatcher.Invoke(() =>
            {
                View.Show();
            });
        }

        public void Shutdown()
        {
            if (_locked) return;
            View.Dispatcher.Invoke(() =>
            {
                View.Close();
            });
            Lock();
        }

        public void WriteLine(object content)
        {
            if (_locked) return;
            Content += $"{content}{Environment.NewLine}";
        }

        public void Dispose()
        {
            if (!_isFinished)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    Finish(App.Current.Resources["LeafUITipUnknow"] as string);
                });
            }
        }
    }
}
