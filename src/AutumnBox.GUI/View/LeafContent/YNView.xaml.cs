using System;
using System.Windows.Controls;
using AutumnBox.GUI.Util.Bus;
using static AutumnBox.GUI.Util.Bus.DialogManager;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// YNView.xaml 的交互逻辑
    /// </summary>
    public partial class YNView : UserControl,IDialog
    {
        public YNView(string content,string btnYes,string btnNo)
        {
            InitializeComponent();
            TBContent.Text = content ?? throw new ArgumentNullException(nameof(content));
            if (btnYes != null) BtnYes.Content = btnYes;
            if (btnNo != null) BtnNo.Content = btnNo;
        }

        public object ViewContent => this;

        public event EventHandler<DialogClosedEventArgs> Closed;

        private void BtnNo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(false));
        }

        private void BtnYes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(true));
        }
    }
}
