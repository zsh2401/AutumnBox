using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using static AutumnBox.GUI.Util.Bus.DialogManager;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// SingleSelectView.xaml 的交互逻辑
    /// </summary>
    public partial class SingleSelectView : UserControl, IDialog
    {
        public ICommand Select { get; set; }
        public SingleSelectView(string hint, object[] options)
        {
            InitializeComponent();
            DataContext = this;
            if (hint != null) TBHint.Text = hint;
            LVOptions.ItemsSource = options;
            Select = new MVVMCommand(p =>
            {
                Closed?.Invoke(this, new DialogClosedEventArgs(p));
            });
        }

        public object ViewContent => this;

        public event EventHandler<DialogClosedEventArgs> Closed;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(null));
        }
    }
}
