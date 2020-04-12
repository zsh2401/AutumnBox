/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:46:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.Management;
using AutumnBox.GUI.Properties;
using System.Reflection;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.View.DialogContent;
using AutumnBox.OpenFramework.Open.LKit;
using AutumnBox.Leafx.Container;
using AutumnBox.Leafx;
using AutumnBox.GUI.Util;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using AutumnBox.Leafx.Container.Support;

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

        public bool IsDeveloperMode => Settings.Default.DeveloperMode;

        public bool ShouldDisplayCmdWindow => Settings.Default.DisplayCmdWindow;

        public Version NewtonsoftJsonVersion => VersionInfos.JsonLib;

        public Version AutumnBoxLoggingVersion => VersionInfos.Logging;

        public Version AutumnBoxGUIVersion => VersionInfos.GUI;

        public Version AutumnBoxBasicVersion => VersionInfos.Basic;

        public IRegisterableLake GlobalLake => (IRegisterableLake)GLake.Lake;

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
            return App.Current.Resources["LanguageCode"].ToString();
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

        private static LoadingWindow loadingWindow;

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
            if (Settings.Default.NotifyOnFinish)
            {
                this.GetComponent<ISoundService>().PlayOK();
            }
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
            ILeafUI result = null;
            App.Current.Dispatcher.Invoke(() =>
            {
                result = new LeafWindow()
                {
                    Owner = App.Current.MainWindow,
                }.DataContext as ILeafUI;
            });
            return result;
        }

        public object GetNewView(string viewId)
        {
            switch (viewId)
            {
                case "inputIpEndPoint":
                    return new ContentConnectNetDevice();
                default:
                    return null;
            }
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

        public void SendNotification(string msg, string title = null, Action clickHandler = null)
        {
            this.GetComponent<INotificationManager>().SendInfo(msg);
        }

        public void SetWindowBlur(IntPtr hWnd)
        {
            Component.Get<IAcrylicHelper>().SetWindowBlur(hWnd); ;
        }
    }
}
