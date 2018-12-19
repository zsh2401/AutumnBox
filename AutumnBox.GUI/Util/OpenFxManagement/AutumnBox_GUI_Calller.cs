/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:46:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.Management;
using AutumnBox.GUI.Util.Effect;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.OpenFramework.Extension;
using System.Reflection;
using System.Security.Policy;
using AutumnBox.Basic.Device;
using AutumnBox.GUI.Util.Bus;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal partial class AutumnBox_GUI_Calller : IBaseApi
    {
        public IDevice SelectedDevice
        {
            get
            {
                return DeviceSelectionObserver.Instance.CurrentDevice;
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

        public IExtensionUIController GetUIController()
        {
            var window = new RunningWindow();
            return window.ViewModel;
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
            SGLogger<AutumnBox_GUI_Calller>.Info("Playing ok");
            if (Settings.Default.NotifyOnFinish)
            {
                Sounds.OK.Play();
            }
        }

        public void PlayErr()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 日志
        /// </summary>
        /// <param name="tagOrSender"></param>
        /// <param name="levelString"></param>
        /// <param name="text"></param>
        public void Log(object tagOrSender, string levelString, string text)
        {
            LoggingStation.Instance.Log(tagOrSender?.ToString() ?? "UnknowClass", levelString, text);
        }
        public void LoadAssemblyToDomain(Assembly assembly)
        {
            AppDomain.CurrentDomain.Load(assembly.FullName);
        }
        public AppDomain GetExtAppDomain()
        {
            Evidence evi = AppDomain.CurrentDomain.Evidence;
            AppDomainSetup ads = new AppDomainSetup()
            {
                ApplicationBase = AppDomain.CurrentDomain.BaseDirectory,
                DisallowBindingRedirects = false,
                DisallowCodeDownload = true,

                ConfigurationFile =
                AppDomain.CurrentDomain.SetupInformation.ConfigurationFile
            };
            var appDomain = AppDomain.CreateDomain("ad", evi, ads);
            var loadedAssembly = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var loaded in loadedAssembly)
            {
                try
                {
                    appDomain.Load(loaded.GetName());
                }
                catch (Exception ex)
                {
                    SGLogger<AutumnBox_GUI_Calller>.Debug(ex);
                }

            }
            return appDomain;
        }

        public IClassExtension GetInstanceFrom(AppDomain appDomain, Type type)
        {
            return (AutumnBoxExtension)appDomain
                .CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
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
    }
}
