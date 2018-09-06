/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:46:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.Log;
using System.Diagnostics;
using System;
using System.Windows;
using AutumnBox.OpenFramework.Warpper;
using AutumnBox.GUI.View.Windows;
using AutumnBox.OpenFramework.Management;

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

        public IExtensionUIController GetUIControllerOf(IExtensionWarpper warpper)
        {
            var window = new RunningWindow(warpper);
            return window.ViewModel;
        }

        public void Restart()
        {
            Application.Current.Shutdown();
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("..\\AutumnBox-秋之盒.exe")
            {
                Arguments = $"-tryadmin -waitatmb"
            };
            Logger.Debug(this, startInfo.FileName + "  " + startInfo.Arguments);
            Process.Start(startInfo);
            Application.Current.Shutdown();
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
            return new View.Windows.LoadingWindow()
            {
                Owner = App.Current.MainWindow
            };
        }
    }
}
