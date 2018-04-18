using AutumnBox.Support.Log;
using System;
using System.Windows;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// DebugWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DebugWindow : Window
    {
        public DebugWindow()
        {
            InitializeComponent();
            TxtBoxLog.AppendText(Logger.logBuffer.ToString());
            TxtBoxLog.ScrollToEnd();
            Logger.Logged += Logger_Logged;
        }

        private void Logger_Logged(object sender, LogEventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                TxtBoxLog.AppendText(e.FullMessage);
                TxtBoxLog.ScrollToEnd();
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Logger.Logged -= Logger_Logged;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TxtBoxLog.ScrollToEnd();
        }
    }
}
