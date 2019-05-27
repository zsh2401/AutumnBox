using System;
using System.Windows.Controls;
using AutumnBox.GUI.Util.Bus;
using static AutumnBox.GUI.Util.Bus.DialogManager;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// ChoiceView.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceView : UserControl,IDialog
    {
        public ChoiceView(string content,string btnYes,string btnNo,string btnCancel)
        {
            InitializeComponent();
            TBContent.Text = content ?? throw new ArgumentNullException(nameof(content));
            if (btnYes != null) BtnYes.Content = btnYes;
            if (btnNo != null) BtnNo.Content = btnNo;
            if (btnCancel != null) BtnCancel.Content = btnCancel;
        }

        public object ViewContent => this;

        public event EventHandler<DialogClosedEventArgs> Closed;

        private void BtnYes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(true));
        }

        private void BtnNo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(false));
        }

        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(null));
        }
    }
}
