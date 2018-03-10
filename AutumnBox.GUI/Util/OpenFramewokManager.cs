/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/25 2:36:04 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.FlowFramework;
using AutumnBox.GUI.Helper;
using AutumnBox.OpenFramework;
using AutumnBox.OpenFramework.Extension;
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
        }

        private class GuiApi : IGuiApi
        {
            public Dispatcher Dispatcher => App.Current.Dispatcher;
            public string CurrentLanguageCode => App.Current.Resources["LanguageCode"].ToString();

            public object GetResouce(string key)
            {
                return App.Current.Resources[key];
            }
            public TReturn GetResouce<TReturn>(string key) where TReturn : class
            {
                return App.Current.Resources[key] as TReturn;
            }

            public bool? ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null)
            {
                bool? result = null;
                App.Current.Dispatcher.Invoke(() =>
                {
                    result = BoxHelper.ShowChoiceDialog(title, msg, btnLeft, btnRight).ToBool();
                });
                return result;
            }
            public void ShowLoadingWindow(ICompletable completable)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    BoxHelper.ShowLoadingDialog(completable);
                });

            }
            public void ShowMessageBox(string title, string msg)
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    BoxHelper.ShowMessageDialog(title, msg);
                });

            }
        }
        private class LogApi : ILogApi
        {
            public void Debug(Context sender, string msg)
            {
                Support.Log.Logger.Debug(sender.Tag, msg);
            }

            public void Info(Context sender, string msg)
            {
                Support.Log.Logger.Info(sender.Tag, msg);
            }

            public void Warn(Context sender, string msg)
            {
                Support.Log.Logger.Warn(sender.Tag, msg);
            }

            public void Warn(Context sender, string msg, Exception e)
            {
                Support.Log.Logger.Warn(sender.Tag, msg, e);
            }
        }
    }
}
