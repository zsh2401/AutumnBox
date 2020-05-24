using System;
using System.Windows.Controls;
using AutumnBox.GUI.Services;


namespace AutumnBox.GUI.Views.SubWindowViews
{
    /// <summary>
    /// MessageView.xaml 的交互逻辑
    /// </summary>
    partial class LeafUIMessageView : UserControl, ISubWindowDialog
    {
        public LeafUIMessageView(string message)
        {
            InitializeComponent();
            TBContent.Text = message;
        }

        public object View => this;

        public event EventHandler<SubWindowFinishedEventArgs> Finished;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(null));
        }
    }
}
