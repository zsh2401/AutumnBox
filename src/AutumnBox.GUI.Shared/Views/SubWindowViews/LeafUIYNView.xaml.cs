using System;
using System.Windows.Controls;
using AutumnBox.GUI.Services;


namespace AutumnBox.GUI.Views.SubWindowViews
{
    /// <summary>
    /// YNView.xaml 的交互逻辑
    /// </summary>
    public partial class LeafUIYNView : UserControl, ISubWindowDialog
    {
        public LeafUIYNView(string content, string btnYes, string btnNo)
        {
            InitializeComponent();
            TBContent.Text = content ?? throw new ArgumentNullException(nameof(content));
            if (btnYes != null) BtnYes.Content = btnYes;
            if (btnNo != null) BtnNo.Content = btnNo;
        }

        public object View => this;

        public event EventHandler<SubWindowFinishedEventArgs> Finished;

        private void BtnNo_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(false));
        }

        private void BtnYes_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(true));
        }
    }
}
