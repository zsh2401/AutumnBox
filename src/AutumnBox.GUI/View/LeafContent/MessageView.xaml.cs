using System;
using System.Windows.Controls;
using AutumnBox.GUI.Util.Bus;
using static AutumnBox.GUI.Util.Bus.DialogManager;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// MessageView.xaml 的交互逻辑
    /// </summary>
    partial class MessageView : UserControl, IDialog
    {
        public MessageView(string message)
        {
            InitializeComponent();
            TBContent.Text = message;
        }

        public object ViewContent => this;

        public event EventHandler<DialogClosedEventArgs> Closed;

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this,new DialogClosedEventArgs());
        }
    }
}
