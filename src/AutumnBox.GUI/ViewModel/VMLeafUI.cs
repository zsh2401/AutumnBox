using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.View.LeafContent;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Open.LKit;
using HandyControl.Controls;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.ViewModel
{
    class VMLeafUI : ViewModelBase, ILeafUI
    {
        public string Token { get; } = Guid.NewGuid().ToString();
        private enum State
        {
            Initing = -1,
            Ready = 0,
            Running = 1,
            Finished = 2,
            Shutdown = 3,
            Unfinished = 4,
        }
        public Visibility LoadingLineVisibility
        {
            get => lv; set
            {
                lv = value;
                RaisePropertyChanged();
            }
        }
        private Visibility lv;

        [AutoInject]
        IOperatingSystemService operatingSystemService = null;

        [AutoInject]
        ISubWindowDialogManager subWindowDialogManager = null;

        public Visibility ProgressBarVisibility
        {
            get => _pbv; set
            {
                _pbv = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _pbv = Visibility.Hidden;

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


        public LeafWindow View
        {
            get => _view; set
            {
                _view = value;
                RaisePropertyChanged();
                BindingViewEvents();
            }
        }
        private LeafWindow _view;

        public string DetailsContent
        {
            get
            {
                return detailsBuilder.ToString();
            }
        }
        private readonly StringBuilder detailsBuilder = new StringBuilder();
        private readonly object detailsLock = new object();
        private void AppendDetails(object content, bool newLineAtEnd = true)
        {
            lock (detailsLock)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    detailsBuilder.Append(newLineAtEnd ? $"{content?.ToString()}\n" : content?.ToString());
                    RaisePropertyChanged(nameof(DetailsContent));
                });
            }
        }

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
                if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
                if (value <= 0)
                {
                    _progress = 0;
                    LoadingLineVisibility = Visibility.Visible;
                    ProgressBarVisibility = Visibility.Hidden;
                    RaisePropertyChanged();
                    return;
                }
                else if (value < 100)
                {
                    LoadingLineVisibility = Visibility.Hidden;
                    ProgressBarVisibility = Visibility.Visible;
                }
                if (value == 100)
                {
                    LoadingLineVisibility = Visibility.Collapsed;
                    ProgressBarVisibility = Visibility.Collapsed;
                }
                _progress = value;
                RaisePropertyChanged();
            }
        }
        private int _progress;

        public string StatusInfo
        {
            get => _tip; set
            {
                if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
                _tip = value;
                RaisePropertyChanged();
            }
        }
        private string _tip;

        public byte[] Icon
        {
            get => _icon; set
            {
                if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
                _icon = value;
                RaisePropertyChanged();
            }
        }
        private byte[] _icon;

        public string Title
        {
            get => _title; set
            {
                if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        public event LeafUIClosingEventHandler Closing;

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

        public bool IsDisplayDetails
        {
            get => _isDisplayDetails; set
            {
                _isDisplayDetails = value;
                RaisePropertyChanged();
            }
        }
        private bool _isDisplayDetails;

        public string StatusDescription
        {
            get => _statusDescription; set
            {
                _statusDescription = value;
                RaisePropertyChanged();
            }
        }
        private string _statusDescription;

        private void BindingViewEvents()
        {
            if (CurrentState != State.Ready) return;
            View.Closing += OnLeafWindowClosing;
        }

        public VMLeafUI()
        {
            if (IsDesignMode())
            {
                return;
            }
            RaisePropertyChangedOnUIThread = true;
            Title = "LeafUI Window";
            Progress = -1;
            StatusInfo = App.Current.Resources["LeafUITipRunning"] as string;
            StatusDescription = "--";
            Icon = null;
            Copy = new FlexiableCommand(() =>
            {
                try
                {
                    Clipboard.SetText(DetailsContent);
                }
                catch { }
            });
            CurrentState = State.Ready;
        }

        private void OnLeafWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished || CurrentState == State.Unfinished)
            {
                e.Cancel = false;
            }
            else
            {
                bool? closeResult = null;
                try
                {
                    SLogger<VMLeafUI>.Info("handling closing event");
                    closeResult = Closing?.Invoke(this, new LeafUIClosingEventArgs());
                    SLogger<VMLeafUI>.Info($"handled closing event {closeResult?.ToString() ?? "null"}");
                }
                catch (Exception ex)
                {
                    AppendDetails(ex);
                }

                if (closeResult == true)
                {
                    e.Cancel = true;
                    Finish(0);
                }
                else
                {
                    e.Cancel = true;
                    AppendDetails(App.Current.Resources["LeafUICannotStop"]);
                }
            }
        }

        public void EnableHelpBtn(Action callback)
        {
            View.Dispatcher.Invoke(() =>
            {
                operatingSystemService.EnableHelpButton(View, callback);
            });
        }

        public void Finish(int exitCode = 0)
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
            App.Current.Dispatcher.Invoke(() =>
            {
                Finish(App.Current.Resources["LeafUITipCode" + exitCode] as string
                    ?? App.Current.Resources["LeafUITipCodeUnknown"] as string);
            });
        }

        public void Finish(string tip)
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
            StatusInfo = tip ?? StatusMessages.Success;
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
            View.Dispatcher.Invoke(() =>
            {
                View?.Close();
            });
        }

        public void Dispose()
        {
            if (CurrentState == State.Finished)
            {
                return;
            }
            else if (CurrentState == State.Running)
            {
                CurrentState = State.Unfinished;
            }
            View?.Dispatcher.Invoke(() =>
            {
                View?.Close();
            });
            View = null;
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
                task = subWindowDialogManager.ShowDialog(Token, new MessageView(message));
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
                task = subWindowDialogManager.ShowDialog(Token, view);
            });
            task.Wait();
            return (task.Result as bool?);
        }

        public object SelectOne(string hint, params object[] options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            Task<object> task = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                var view = new SingleSelectView(hint, options);
                task = subWindowDialogManager.ShowDialog(Token, view);
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

        public Task<object> ShowLeafDialog(ILeafDialog dialog)
        {
            var hDialog = Dialog.Show(dialog.ViewContent, Token);
            return Task.Run(() =>
            {
                object result = null;
                bool closed = false;
                dialog.RequestedClose += (s, e) =>
                {
                    closed = true;
                    App.Current.Dispatcher.Invoke(() => { hDialog.Close(); });
                    result = e.Result;
                };
                while (!closed)
                {
                    Thread.Sleep(10);
                }
                return result;
            });
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
                task = subWindowDialogManager.ShowDialog(Token, view);
            });
            task.Wait();
            return (bool)task.Result;
        }

        public string InputString(string hint = null, string _default = null)
        {
            Task<object> task = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                var view = new InputView(hint, _default);
                task = subWindowDialogManager.ShowDialog(Token, view);
            });
            task.Wait();
            return task.Result as string;
        }

        public void ShowDialog()
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

        public void WriteLineToDetails(string _str)
        {
            AppendDetails(_str, true);
        }

        public void WriteToDetails(string _str)
        {
            AppendDetails(_str, false);
        }
    }
}
