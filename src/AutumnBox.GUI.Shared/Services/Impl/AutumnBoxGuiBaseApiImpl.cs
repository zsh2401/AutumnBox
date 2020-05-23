/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:46:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.GUI.Views.Windows;
using AutumnBox.OpenFramework.Management;
using System.Reflection;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open.LKit;
using AutumnBox.Leafx.Container;
using AutumnBox.GUI.Util;
using AutumnBox.Leafx.Container.Support;
using System.Threading.Tasks;
using System.IO;
using AutumnBox.GUI.Views.Windows;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IBaseApi))]
    internal class AutumnBoxGuiBaseApiImpl : IBaseApi
    {

        public IDevice SelectedDevice
        {
            get
            {
                return this.GetComponent<IAdbDevicesManager>().SelectedDevice;
            }
        }

        public Version Version
        {
            get
            {
                return Self.Version;
            }
        }

        public bool IsRunAsAdmin => Self.HaveAdminPermission;

        public bool IsDeveloperMode => this.GetComponent<ISettings>().DeveloperMode;

        public bool ShouldDisplayCmdWindow => false;

        public Version AutumnBoxLoggingVersion => VersionInfos.Logging;

        public Version AutumnBoxGUIVersion => VersionInfos.GUI;

        public Version AutumnBoxBasicVersion => VersionInfos.Basic;

        public IRegisterableLake GlobalLake => (IRegisterableLake)App.Current.Lake;

        public DirectoryInfo StorageDirectory => this.GetComponent<IStorageManager>().StorageDirectory;

        public DirectoryInfo TempDirectory => this.GetComponent<IStorageManager>().CacheDirectory;

        public void ShowDebugUI()
        {
            new LogWindow().Show();
        }

        public void ShowMessage(string title, string msg)
        {
            new MessageWindow()
            {
                Owner = App.Current.MainWindow,
                MsgTitle = title,
                Message = msg,
            }.ShowDialog();
        }

        public string GetCurrentLanguageCode()
        {
            return this.GetComponent<ILanguageManager>().Current.Code;
        }

        public dynamic GetMainWindow()
        {
            return App.Current.MainWindow;
        }

        public object GetResouce(string key)
        {
            return App.Current.Resources[key];
        }


        public void Restart()
        {
            Self.Restart(false);
        }

        public void RestartAsAdmin()
        {
            Self.Restart(true);
        }

        public void RunOnUIThread(Action act)
        {
            App.Current.Dispatcher.Invoke(act);
        }

        public int DoChoice(string msg, string btnLeft = null, string btnRight = null, string btnCancel = null)
        {
            var window = new ChoiceWindow()
            {
                Owner = App.Current.MainWindow,
                Message = msg
            };
            window.BtnLeft = btnLeft ?? window.BtnLeft;
            window.BtnCancel = btnCancel ?? window.BtnCancel;
            window.BtnRight = btnRight ?? window.BtnRight;
            window.ShowDialog();
            return window.ClickedBtn;
        }

        public void Shutdown()
        {
            App.Current.Shutdown();
        }

        public AutumnBoxGuiBaseApiImpl()
        {
            this.GetComponent<ILanguageManager>().LanguageChanged += (s, e) =>
            {
                LanguageChanged?.Invoke(this, new EventArgs());
            };
        }

        private static LoadingWindow loadingWindow;

        public event EventHandler LanguageChanged;

        public event EventHandler Destorying
        {
            add
            {
                App.Current.Exit += (s, e) => value(s, new EventArgs());
            }
            remove
            {
                //TO DO
            }
        }

        public void ShowLoadingUI()
        {
            if (loadingWindow != null) return;
            loadingWindow = new LoadingWindow()
            {
                Owner = App.Current.MainWindow
            };
            loadingWindow.ShowDialog();
        }
        public void CloseLoadingUI()
        {
            if (loadingWindow == null) return;
            loadingWindow.Close();
            loadingWindow = null;
        }


        public void PlayOk()
        {
            this.GetComponent<ISoundService>().PlayOK();
        }

        public void PlayErr()
        {
            throw new NotImplementedException();
        }

        public void LoadAssemblyToDomain(Assembly assembly)
        {
            AppDomain.CurrentDomain.Load(assembly.FullName);
        }

        public bool DoYN(string message, string btnYes, string btnNo)
        {
            var window = new YNWindow()
            {
                Owner = App.Current.MainWindow,
                BtnYES = btnYes ?? "OpenFxBtnYes",
                BtnNO = btnNo ?? "OpenFxBtnNo",
                Message = message,
            };
            window.ShowDialog();
            return window.DialogResult == true;
        }

        public bool InputNumber(string hint, int min, int max, out int result)
        {
            int tmp = 0;
            string _hint = App.Current.Resources[hint] as string ?? hint;
            _hint += $"({min}~{max})";
            var window = new InputWindow(_hint);
            window.InputCheck = (str) =>
            {
                if (int.TryParse(str, out tmp))
                {
                    return min <= tmp && tmp <= max;
                }
                else
                {
                    return false;
                }
            };
            window.ShowDialog();
            result = tmp;
            return window.DialogResult == true;
        }

        public bool InputString(string hint, out string result)
        {
            string tmp = null;
            var window = new InputWindow(hint);
            window.InputCheck = (str) =>
            {
                tmp = str;
                return true;
            };
            window.ShowDialog();
            result = tmp;
            return window.DialogResult == true;
        }

        public void SetResource(string key, object value)
        {
            App.Current.Resources[key] = value;
        }

        public void AddResource(string key, object value)
        {
            App.Current.Resources.Add(key, value);
        }

        public void ShowException(string title, string sketch, string exceptionMessage)
        {
            new ExceptionWindow()
            {
                MessageTitle = title,
                ExceptionMessage = exceptionMessage,
                Sketch = sketch,
                Owner = App.Current.MainWindow
            }.Show();
        }

        public ILeafUI NewLeafUI()
        {
            return App.Current.Dispatcher.Invoke(() =>
            {
                return new LeafWindow()
                {
                    Owner = App.Current.MainWindow,
                }.DataContext as ILeafUI;
            });
        }

        public object GetNewView(string viewId)
        {
            throw new NotSupportedException();
        }


        public void RefreshExtensionList()
        {
            this.GetComponent<IMessageBus>().SendMessage(Messages.REFRESH_EXTENSIONS_VIEW);
        }

        public void AppendPanel(object view, int priority)
        {
            this.GetComponent<ILeafCardManager>().Add(view, priority);
        }

        public void RemovePanel(object view)
        {
            this.GetComponent<ILeafCardManager>().Remove(view);
        }

        public void SetWindowBlur(IntPtr hWnd)
        {
            this.GetComponent<IAcrylicHelper>().SetWindowBlur(hWnd); ;
        }

        public void SendNotificationInfo(string msg)
        {
            this.GetComponent<INotificationManager>().Info(msg);
        }

        public void SendNotificationSuccess(string msg)
        {
            this.GetComponent<INotificationManager>().Success(msg);
        }

        public void SendNotificationWarn(string msg)
        {
            this.GetComponent<INotificationManager>().Warn(msg);
        }

        public Task<bool> SendNotificationAsk(string msg)
        {
            return this.GetComponent<INotificationManager>().Ask(msg);
        }

        public object UnstableInternalApiCall(string message, object arg = null)
        {
            switch (message)
            {
                case "show_donate_window":
                    this.GetComponent<IWindowManager>().Show("Donate");
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
