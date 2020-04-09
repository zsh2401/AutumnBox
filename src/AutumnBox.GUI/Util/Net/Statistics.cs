/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/11 15:25:53 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.Logging;
using System.Windows.Controls;

namespace AutumnBox.GUI.Util.Net
{
    static class Statistics
    {
        private static ILogger logger = LoggerFactory.Auto(nameof(Statistics));

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
                    MaxWidth = 1,
                };
                (App.Current.MainWindow.Content as Grid).Children.Add(browser);
                browser.SuppressScriptErrors(true);
                string url = App.Current.Resources["WebApiStatistics"]?.ToString();
                url = $"{url}?v={Self.Version.ToString()}&t={VER_TYPE}";
                //logger.Info("Statistics browser is navigating to " + url);
                browser.Navigate(url);
            });
        }
        public const string VER_TYPE =
#if PREVIEW
            "preview";
#else
            "release";
#endif
    }
}
