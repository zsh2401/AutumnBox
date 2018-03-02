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
using System.Windows.Threading;

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
            public Dispatcher Dispatcher => App.Current.Dispatcher;
            public string CurrentLanguageCode => App.Current.Resources["LanguageCode"].ToString();
            public bool? ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null)
            {
                bool? result = null;
                App.Current.Dispatcher.Invoke(()=> {
                    result = BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight).ToBool();
                });
                return result;
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
