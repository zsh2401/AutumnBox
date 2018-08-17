/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/2 2:46:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.UI.FuncPanels;
using AutumnBox.GUI.Windows;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi;
using AutumnBox.Support.Log;
using System.Diagnostics;
using System;
using System.Windows;
using AutumnBox.GUI.Util.UI;
using AutumnBox.OpenFramework.Warpper;
using AutumnBox.GUI.View.Windows;

namespace AutumnBox.GUI.Util.OpenFxManagement
{
    internal partial class AppManagerImpl : IAutumnBoxGuiApi
    {
        public Version Version
        {
            get
            {
                return Self.Version;
            }
        }


        public void CloseLoadingWindow()
        {
            BoxHelper.CloseLoadingDialog();
        }

        public Window CreateDebugWindow()
        {
            return new DebugWindow();
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

        public void RefreshExtensionList()
        {
            ThridPartyFunctionPanel.Single.Refresh();
        }

        public void Restart()
        {
            Application.Current.Shutdown();
            Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

        public void RestartAsAdmin()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("..\\AutumnBox-秋之盒.exe");
            startInfo.Arguments = $"-tryadmin -waitatmb";
            Logger.Debug(this, startInfo.FileName + "  " + startInfo.Arguments);
            Process.Start(startInfo);
            Application.Current.Shutdown();
        }

        public void RunOnUIThread(Action act)
        {
            App.Current.Dispatcher.Invoke(act);
        }
        public ChoiceBoxResult ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null)
        {
            var result = BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight);
            switch (result)
            {
                case ChoiceResult.BtnLeft:
                    return ChoiceBoxResult.Left;
                case ChoiceResult.BtnRight:
                    return ChoiceBoxResult.Right;
                default:
                    return ChoiceBoxResult.Cancel;
            }
        }

        public void ShowLoadingWindow()
        {
            BoxHelper.ShowLoadingDialog();
        }

        public void ShowMessageBox(string title, string msg)
        {
            BoxHelper.ShowMessageDialog(title, msg);
        }

        public void Shutdown()
        {
            App.Current.Shutdown();
        }
    }
}
