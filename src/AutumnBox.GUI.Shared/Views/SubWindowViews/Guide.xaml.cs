using System;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.GUI.Services;


namespace AutumnBox.GUI.Views.SubWindowViews
{
    /// <summary>
    /// ContentGuide.xaml 的交互逻辑
    /// </summary>
    public partial class Guide : UserControl, ISubWindowDialog
    {
        public Guide()
        {
            InitializeComponent();
        }

        public object View => this;

        public event EventHandler<SubWindowFinishedEventArgs> Finished;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(false));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(true));
        }
    }
}
