using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.View.LeafContent;
using AutumnBox.GUI.View.Windows;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension.Leaf;
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
        public string Token { get; }
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
                if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
                if (value == -1)
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
                }
                _progress = value;
                RaisePropertyChanged();
            }
        }
        private int _progress;

        public string Tip
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
            Token = Guid.NewGuid().ToString();
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


        private void View_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                    WriteLine(ex);
                }

                if (closeResult == true)
                {
                    e.Cancel = true;
                    Finish(0);
                }
                else
                {
                    e.Cancel = true;
                    WriteLine(App.Current.Resources["LeafUICannotStop"]);
                }
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
            App.Current.Dispatcher.Invoke(() =>
            {
                View?.Close();
            });
        }

        public void WriteLine(object content)
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
            _contentBuilder.AppendLine(content?.ToString());
            RaisePropertyChanged(nameof(Content));
            WriteOutput(content?.ToString());
        }

        public void WriteOutput(object output)
        {
            if (CurrentState == State.Shutdown || CurrentState == State.Finished) return;
            _fullContentBuilder.AppendLine(output?.ToString());
            RaisePropertyChanged(nameof(FullContent));
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
                task = DialogManager.Show(Token, new MessageView(message));
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
                task = DialogManager.Show(Token, view);
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
                task = DialogManager.Show(Token, view);
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
                dialog.Closed += (s, e) =>
                {
                    App.Current.Dispatcher.Invoke(() => { hDialog.Close(); });
                    result = e.Result;
                };
                while (!App.Current.Dispatcher.Invoke(() => hDialog.IsClosed))
                {
                    Thread.Sleep(200);
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
                task = DialogManager.Show(Token, view);
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
                task = DialogManager.Show(Token, view);
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
    }
}
