using AutumnBox.Updater.Core;
using System;
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

        public void Finish()
        {
            Dispatcher.Invoke(Close);
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
    }
}
