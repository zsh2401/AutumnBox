using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.View.LeafContent;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.LeafExtension.Kit;
using MaterialDesignThemes.Wpf;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.ViewModel
{
    class VMLeafUI : ViewModelBase, ILeafUI
    {
        private enum State
        {
            Initing = -1,
            Ready = 0,
            Running = 1,
            Finished = 2,
            Shutdown = 3,
            Unfinished = 4,
        }
        private State CurrentState { get; set; } = State.Initing;

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

        public LeafWindow View
        {
            get => _view; set
            {
                _view = value;
                RaisePropertyChanged();
                InitView();
            }
        }
        private LeafWindow _view;

        public string Content
        {
            get => _contentBuilder?.ToString();
            set { }
        }
        private readonly StringBuilder _contentBuilder;

        public string FullContent
        {
            get => _fullContentBuilder?.ToString();
            set { }
        }
        private readonly StringBuilder _fullContentBuilder;

        public bool EnableFullContent
        {
            get => _enableFullContent; set
            {
                _enableFullContent = value;
                RaisePropertyChanged();
            }
        }
        private bool _enableFullContent = false;

        public int Progress
        {
            get => _progress; set
            {
                ThrowIfNotRunning();
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
        private int _progress;

        public string Tip
        {
            get => _tip; set
            {
                ThrowIfNotRunning();
                _tip = value;
                RaisePropertyChanged();
            }
        }
        private string _tip;

        public byte[] Icon
        {
            get => _icon; set
            {
                ThrowIfNotRunning();
                _icon = value;
                RaisePropertyChanged();
            }
        }
        private byte[] _icon;

        public string Title
        {
            get => _title; set
            {
                ThrowIfNotRunning();
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        public event EventHandler<LeafCloseBtnClickedEventArgs> CloseButtonClicked;

        private Panel InnerPanel { get; set; }

        public bool ProOutputVisible
        {
            get => EnableFullContent;
            set => EnableFullContent = value;
        }

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged();
            }
        }
        private int _height;

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                RaisePropertyChanged();
            }
        }
        private int _width;

        private void InitView()
        {
            if (CurrentState != State.Ready) return;
            View.Closing += View_Closing;
            InnerPanel = (View.Content as Panel);
        }

        public VMLeafUI()
        {
            _contentBuilder = new StringBuilder();
            _fullContentBuilder = new StringBuilder();
            RaisePropertyChangedOnDispatcher = true;
            Title = "LeafUI Window";
            Progress = -1;
            Tip = App.Current.Resources["LeafUITipRunning"] as string;
            Icon = null;
            Copy = new FlexiableCommand(() =>
            {
                try
                {
                    Clipboard.SetText(FullContent);
                }
                catch { }
            });
            CurrentState = State.Ready;
        }

        public object _GetDialogHost()
        {
            return View.DialogHost;
        }

        private void View_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished || CurrentState == State.Unfinished)
            {
                e.Cancel = false;
            }
            else
            {
                LeafCloseBtnClickedEventArgs args = new LeafCloseBtnClickedEventArgs
                {
                    CanBeClosed = false
                };
                CloseButtonClicked?.Invoke(this, args);
                e.Cancel = !args.CanBeClosed;
                if (args.CanBeClosed) Finish();
                else WriteLine(App.Current.Resources["RunningWindowCantStop"]);
            }
        }

        public void EnableHelpBtn(Action callback)
        {
            View.Dispatcher.Invoke(() =>
            {
                HelpButtonHelper.EnableHelpButton(View, callback);
            });
        }

        public void Finish(int exitCode = 0)
        {
            ThrowIfNotRunning();
            App.Current.Dispatcher.Invoke(() =>
            {
                Finish(App.Current.Resources["LeafUITipCode" + exitCode] as string
                    ?? App.Current.Resources["LeafUITipCodeUnknown"] as string);
            });
        }

        public void Finish(string tip)
        {
            ThrowIfNotRunning();
            Tip = tip;
            Progress = 100;
            CurrentState = State.Finished;
        }

        public void Show()
        {
            if (CurrentState != State.Ready)
            {
                throw new InvalidOperationException("Leaf UI is not ready!");
            }
            View.Dispatcher.Invoke(() =>
            {
                View.Show();
            });
            CurrentState = State.Running;
        }

        public void Shutdown()
        {
            CurrentState = State.Shutdown;
        }

        public void WriteLine(object content)
        {
            ThrowIfNotRunning();
            _contentBuilder.AppendLine(content?.ToString());
            RaisePropertyChanged(nameof(Content));
            WriteOutput(content?.ToString());
        }

        public void WriteOutput(object output)
        {
            ThrowIfNotRunning();
            _fullContentBuilder.AppendLine(output?.ToString());
            RaisePropertyChanged(nameof(FullContent));
        }

        public void Dispose()
        {
            //Trace.WriteLine("LeafUI dispose");
            if (CurrentState == State.Finished)
            {
                return;
            }
            else if (CurrentState == State.Running)
            {
                CurrentState = State.Unfinished;
            }
            View.Dispatcher.Invoke(() =>
            {
                View.Close();
            });
            View = null;
        }

        private void ThrowIfNotRunning()
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished)
            {
                throw new InvalidOperationException("Leaf UI is locked!");
            }
        }

        public void ShowMessage(string message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Task<object> task = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                var view = new MessageView(message);
                task = View.DialogHost.ShowDialog(view);
            });
            task.Wait();
        }

        public bool? DoChoice(string message, string btnYes = null, string btnNo = null, string btnCancel = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Task<object> task = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                var view = new ChoiceView(message, btnYes, btnNo, btnCancel);
                task = View.DialogHost.ShowDialog(view);
            });
            task.Wait();
            return (task.Result as bool?);
        }

        public object SelectFrom(string hint, params object[] options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            Task<object> task = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                var view = new SingleSelectView(hint, options);
                task = View.DialogHost.ShowDialog(view);
            });
            task.Wait();
            return task.Result;
        }

        public object[] Select(object[] option, int maxSelect = 1)
        {
            throw new NotImplementedException();
        }

        public void RunOnUIThread(Action act)
        {
            if (act == null)
            {
                throw new ArgumentNullException(nameof(act));
            }

            App.Current.Dispatcher.Invoke(() =>
            {
                act();
            });
        }

        public Task<object> _ShowDialog(object content)
        {
            Task<object> dialogTask = null;
            RunOnUIThread(() =>
            {
                dialogTask = View.DialogHost.ShowDialog(content);
            });
            return dialogTask;
        }

        public bool DoYN(string message, string btnYes = null, string btnNo = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Task<object> task = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                var view = new YNView(message, btnYes, btnNo);
                task = View.DialogHost.ShowDialog(view);
            });
            task.Wait();
            return (bool)task.Result;
        }
    }
}
