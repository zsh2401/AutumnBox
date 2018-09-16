using AutumnBox.GUI.Util.Debugging;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// LogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
            TextBox.AppendText(LoggingStation.Instance.CurrentLogged);
            TextBox.ScrollToEnd();
            LoggingStation.Instance.Logging += Logger_Logged;
        }

        private void Logger_Logged(object sender, LogEventArgs e)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                TextBox.AppendText(e.Content);
                TextBox.ScrollToEnd();
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.ScrollToEnd();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            LoggingStation.Instance.Logging -= Logger_Logged;
        }
    }
}
