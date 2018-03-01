/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.GUI.Helper;
using AutumnBox.OpenFramework.Internal;
using AutumnBox.OpenFramework.Open.V1;
using System;
using System.Windows;

namespace AutumnBox.GUI.Util
{
    internal static class OpenFramewokManager
    {

        public static void LoadApi()
        {
            OpenApi.Gui = new GuiApi();
            OpenApi.Log = new LogApi();
            ExtensionManager.LoadAllExtension();
        }
        
        private class GuiApi : IGuiApi
        {
            public string GetCurrentLanguageName()
            {
                switch (App.Current.Resources["LanguageName"]) {
                    case "简体中文":
                        return "zh-CN";
                    case "English":
                    default:
                        return "en-US";
                }
            }

            public bool? ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null)
            {
                bool? result = null;
                App.Current.Dispatcher.Invoke(()=> {
                    result = BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight).ToBool();
                });
                return result;
            }

            public void ShowExceptionBox(string title, Exception e)
            {
                ShowMessageBox(title,"a exception happend\n" + e);
            }

            public void ShowLoadingWindow(ICompletable completable)
            {
                App.Current.Dispatcher.Invoke(() => {
                    BoxHelper.ShowLoadingDialog(completable);
                });
                
            }
            public void ShowMessageBox(string title, string msg)
            {
                App.Current.Dispatcher.Invoke(() => {
                    BoxHelper.ShowMessageDialog(title, msg);
                });
                
            }
            public void ShowWindow(Window window)
            {
                App.Current.Dispatcher.Invoke(()=> {
                    window.Show();
                });
            }
        }
        private class LogApi : ILogApi
        {
            public void Log(string tag, string msg)
            {
                Support.Log.Logger.Info(tag + "(ExtModule)", msg);
            }
        }
    }
}
