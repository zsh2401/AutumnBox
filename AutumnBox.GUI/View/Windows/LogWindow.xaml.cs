using AutumnBox.GUI.ViewModel;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            TextBox.AppendText(Logger.logBuffer.ToString());
            TextBox.ScrollToEnd();
            Logger.Logged += Logger_Logged;
        }

        private void Logger_Logged(object sender, LogEventArgs e)
        {
            TextBox.AppendText(e.FullMessage);
            TextBox.ScrollToEnd();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBox.ScrollToEnd();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Logger.Logged -= Logger_Logged;
        }
    }
}
