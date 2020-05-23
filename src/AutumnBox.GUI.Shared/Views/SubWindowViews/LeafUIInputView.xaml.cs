using AutumnBox.GUI.Services;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.Views.SubWindowViews
{
    /// <summary>
    /// InputView.xaml 的交互逻辑
    /// </summary>
    public partial class LeafUIInputView : UserControl, ISubWindowDialog
    {
        public LeafUIInputView(string hint, string _default)
        {
            InitializeComponent();
            TBHint.Text = hint;
            TBInput.Text = _default;
        }

        public object View => this;

        public event EventHandler<SubWindowFinishedEventArgs> Finished;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(TBInput.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(null));
        }
    }
}
