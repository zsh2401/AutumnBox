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
using AutumnBox.GUI.Util.Bus;
using static AutumnBox.GUI.Util.Bus.DialogManager;

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// InputView.xaml 的交互逻辑
    /// </summary>
    public partial class InputView : UserControl, IDialog
    {
        public InputView(string hint, string _default)
        {
            InitializeComponent();
            TBHint.Text = hint;
            TBInput.Text = _default;
        }

        public object ViewContent => this;

        public event EventHandler<DialogClosedEventArgs> Closed;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(TBInput.Text));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(null));
        }
    }
}
