/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:25:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using System.Windows.Controls;

namespace AutumnBox.GUI.Util
{
    static class Statistics
    {
        private const string TAG = "Statistics";
        private static ILogger logger = new Logger(TAG);

        [System.Diagnostics.Conditional("STAT")]
        public static void Do()
        {
            logger.Debug("Doing");
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
