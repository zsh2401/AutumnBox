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

        public Window CreateDebugWindow()
        {
            return new LogWindow();
        }

        public Window CreateMessageWindow(string title, string msg)
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

        public Window GetMainWindow()
        {
            return App.Current.MainWindow;
        }

        public object GetResouce(string key)
        {
            return App.Current.Resources[key];
        }

        public IExtensionUIController GetUIControllerOf(IExtensionWrapper wrapper)
        {
            var window = new RunningWindow(wrapper);
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

        public Window CreateChoiceWindow(string msg, string btnLeft = null, string btnRight = null, string btnCancel = null)
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

        public Window CreateLoadingWindow()
        {
            return new LoadingWindow()
            {
                Owner = App.Current.MainWindow
            };
        }

        public void PlayOk()
        {
            Sounds.OK.Play();
        }

        public void PlayErr()
        {
            throw new NotImplementedException();
        }

        public bool PlaySoundWhenFinished()
        {
            return Settings.Default.NotifyOnFinish;
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
    }
}
