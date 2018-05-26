using AutumnBox.Updater.Core;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
namespace AutumnBox.Updater
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IProgressWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private bool closeByProgram = false;
        public void Finish()
        {
            closeByProgram = true;
            Dispatcher.Invoke(Close);
            Process.Start("AutumnBox-秋之盒.exe");
        }

        public void SetProgress(double value)
        {
            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = value;
            });
        }

        public void AppendLog(string text)
        {
            Dispatcher.Invoke(() =>
            {
                TBLog.AppendText(text + Environment.NewLine);
                TBLog.ScrollToEnd();
            });
        }

        public void AppendLog(string text, double value)
        {
            Dispatcher.Invoke(() =>
            {
                AppendLog(text);
                SetProgress(value);
            });

        }

        public void SetUpdateContent(string text)
        {
            Dispatcher.Invoke(() =>
            {
                TBUpdateContent.Text = text;
            });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Run(() =>
            {
                UpdaterCore.Updater.Start(this);
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeByProgram)
            {
                MessageBoxResult result =
                    MessageBox.Show("真的要关闭?如果更新未完成就关闭,将会导致程序损坏!",
                    "警告", MessageBoxButton.YesNo);
                e.Cancel = !(result == MessageBoxResult.Yes);
            }
        }
    }
}
