/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:46:15 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Windows;
using AutumnBox.OpenFramework.Wrapper;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.Management;
using AutumnBox.GUI.Util.Effect;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.OpenFramework.Extension;
using System.Reflection;
using System.Security.Policy;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal partial class AutumnBox_GUI_Calller : IAutumnBox_GUI
    {
        public Version Version
        {
            get
            {
                return Self.Version;
            }
        }

        public bool IsRunAsAdmin => Self.HaveAdminPermission;

        public dynamic CreateDebugWindow()
        {
            return new LogWindow();
        }

        public dynamic CreateMessageWindow(string title, string msg)
        {
            return new MessageWindow()
            {
                Owner = App.Current.MainWindow,
                MsgTitle = title,
                Message = msg,
            };
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

        public dynamic CreateChoiceWindow(string msg, string btnLeft = null, string btnRight = null, string btnCancel = null)
        {
            var window = new ChoiceWindow()
            {
                Owner = App.Current.MainWindow,
                Message = msg
            };
            window.BtnLeft = btnLeft ?? window.BtnLeft;
            window.BtnCancel = btnCancel ?? window.BtnCancel;
            window.BtnRight = btnRight ?? window.BtnRight;
            return window;
        }

        public void Shutdown()
        {
            App.Current.Shutdown();
        }

        public dynamic CreateLoadingWindow()
        {
            return new LoadingWindow()
            {
                Owner = App.Current.MainWindow
            };
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

        public AutumnBoxExtension GetInstanceFrom(AppDomain appDomain, Type type)
        {
            return (AutumnBoxExtension)appDomain
                .CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
        }

        public dynamic GetYNWindow(string message, string btnYes, string btnNo)
        {
            return new YNWindow()
            {
                Owner = App.Current.MainWindow,
                BtnYES = btnYes,
                BtnNO = btnNo,
                Message = message,
            };
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
    }
}
