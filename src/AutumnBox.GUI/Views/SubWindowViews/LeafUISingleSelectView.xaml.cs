using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;


namespace AutumnBox.GUI.Views.SubWindowViews
{
    /// <summary>
    /// SingleSelectView.xaml 的交互逻辑
    /// </summary>
    public partial class LeafUISingleSelectView : UserControl, ISubWindowDialog
    {
        public ICommand Select { get; set; }
        public LeafUISingleSelectView(string hint, object[] options)
        {
            InitializeComponent();
            DataContext = this;
            if (hint != null) TBHint.Text = hint;
            LVOptions.ItemsSource = options;
            Select = new MVVMCommand(p =>
            {
                Finished?.Invoke(this, new SubWindowFinishedEventArgs(p));
            });
        }

        public object View => this;

        public event EventHandler<SubWindowFinishedEventArgs> Finished;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(null));
        }
    }
}
