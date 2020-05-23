using AutumnBox.GUI.Services;
using System;
using System.Windows.Controls;

namespace AutumnBox.GUI.Views.SubWindowViews
{
    /// <summary>
    /// ChoiceView.xaml 的交互逻辑
    /// </summary>
    public partial class LeafUIChoiceView : UserControl, ISubWindowDialog
    {
        public object View => this;

        public LeafUIChoiceView(string content, string btnYes, string btnNo, string btnCancel)
        {
            InitializeComponent();
            TBContent.Text = content ?? throw new ArgumentNullException(nameof(content));
            if (btnYes != null) BtnYes.Content = btnYes;
            if (btnNo != null) BtnNo.Content = btnNo;
            if (btnCancel != null) BtnCancel.Content = btnCancel;
        }

        public event EventHandler<SubWindowFinishedEventArgs> Finished;

        private void BtnYes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(true));
        }

        private void BtnNo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(false));
        }

        private void BtnCancel_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(null));
        }
    }
}
