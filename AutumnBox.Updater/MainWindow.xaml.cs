using AutumnBox.Updater.Core;
using System;
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
            this.Close();
        }

        public void SetProgress(double value)
        {
            ProgressBar.Value = value;
        }

        public void SetTip(string text)
        {
            TBTip.Text = text;
        }

        public void SetTip(string text, double value)
        {
            SetTip(text);
            SetProgress(value);
        }

        public void SetTipColor(Color color)
        {
            TBTip.Foreground = new SolidColorBrush(color);
        }

        public void SetUpdateContent(string text)
        {
            TBUpdateContent.Text = text;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.UpdaterCore.Start(this);
        }
    }
}
