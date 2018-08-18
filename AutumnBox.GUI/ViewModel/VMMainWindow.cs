/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 19:04:29 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using AutumnBox.GUI.Depending;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.OpenFxManagement;
using AutumnBox.GUI.Util.UI;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.OpenFramework;
using AutumnBox.Support.Log;
using MaterialDesignThemes.Wpf;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using static AutumnBox.GUI.Depending.ListenerManager;

namespace AutumnBox.GUI.ViewModel
{
    class VMMainWindow : ViewModelBase, ILanguageChangedListener, INotifyFxLoaded
    {
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

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
        public VMMainWindow()
        {
            InitTitle();
        }
        private void InitTitle()
        {
#if DEBUG
            string comp = "Debug";
#else
            string comp = "Release";
#endif
            Title = $"{App.Current.Resources["AppName"]}-{Self.Version}-{comp}";
        }

        public void OnLanguageChanged(LangChangedEventArgs args)
        {
            InitTitle();
        }

        public double Progress
        {
            get => progress; set
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    progress = value;
                    RaisePropertyChanged();
                });
            }
        }
        private double progress = 10;

        public string LoadingTip
        {
            get => loadingTip; set
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    loadingTip = value;
                    RaisePropertyChanged();
                });

            }
        }
        private string loadingTip;

        public int TranSelectIndex
        {
            get => tranIndex; set
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    tranIndex = value;
                    RaisePropertyChanged();
                });
            }
        }
        private int tranIndex;

        public event EventHandler FxLoaded;

        public void LoadAsync(Action callback = null)
        {
            Task.Run(() =>
            {
                Load();
                App.Current.Dispatcher.Invoke(() =>
                {
                    callback?.Invoke();
                });
            });
        }
        private void Load()
        {
            Progress = 0; 
            //如果设置在启动时打开调试窗口
            if (Settings.Default.ShowDebuggingWindowNextLaunch)
            {
                //打开调试窗口
                App.Current.Dispatcher.Invoke(() =>
                {
                    new Windows.DebugWindow().Show();
                });
            }
            Logger.Info(this, $"Run as " + (Self.HaveAdminPermission ? "Admin" : "Normal user"));
            Logger.Info(this, $"AutumnBox version: {Self.Version}");
            Logger.Info(this, $"SDK version: {BuildInfo.SDK_VERSION}");
            Logger.Info(this, $"Windows version {Environment.OSVersion.Version}");
            Progress = 30;
            LoadingTip = App.Current.Resources["ldmsgStartAdb"].ToString();
            bool success = false;
            bool tryAgain = true;
            while (!success)
            {
                Logger.Info(this, "Try to start adb server ");
                success = AdbHelper.StartServer();
                Logger.Info(this, success ? "adb server starts success" : "adb server starts failed...");
                if (!success)
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        tryAgain = BoxHelper.ShowChoiceDialog(
                        "msgWarning",
                        "msgStartAdbServerFail",
                        "btnExit", "btnIHaveCloseOtherPhoneHelper").ToBool();
                    });
                if (tryAgain)
                {
                    Thread.Sleep(2000);
                }
                else
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        App.Current.Shutdown(App.HAVE_OTHER_PROCESS);
                    });
                }
            }
            Progress = 60;
            LoadingTip = App.Current.Resources["ldmsgLoadingExtensions"].ToString();
            OpenFrameworkManager.Init();
            FxLoaded?.Invoke(this, new EventArgs());
            Progress = 100;
            LoadingTip = "Enjoy!";
            Thread.Sleep(1 * 1000);
            TranSelectIndex = 1;
        }
    }
}
