/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:25:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutumnBox.GUI.Util
{
    static class Statistics
    {
        private const string TAG = "Statistics";
        public static void Do()
        {
            Logger.Info(TAG,"Doing");
            App.Current.Dispatcher.Invoke(() =>
            {
                var browser = new WebBrowser
                {
                    Width = 1,
                    Height = 1,
                    MaxHeight = 1,
                    MaxWidth = 1
                };
                (App.Current.MainWindow.Content as Grid).Children.Add(browser);
                browser.SuppressScriptErrors(true);
                browser.Navigate(App.Current.Resources["urlApiStatistics"].ToString());
            });
        }
    }
}
