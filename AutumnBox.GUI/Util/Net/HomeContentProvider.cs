using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.Util.Net
{
    static class HomeContentProvider
    {
        private const string HOST_NAME = "localhost";
        private const string URL = "http://baidu.com";
        static HomeContentProvider()
        {

        }
        public static void AfterLoaded(Action<object> callback)
        {
            Task.Run(() =>
            {
                return new Ping().Send(HOST_NAME);
            }).ContinueWith(t =>
            {
                if (t.IsCompleted && !t.IsFaulted && !t.IsCanceled && t.Result.Status == IPStatus.Success)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        var webBrowser = new WebBrowser();
                        webBrowser.SuppressScriptErrors(true);
                        webBrowser.Navigate(URL);
                        callback(webBrowser);
                    });
                }
                else
                {
                    callback(null);
                }
            });
        }
    }
}
