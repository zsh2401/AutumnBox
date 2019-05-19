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

namespace AutumnBox.GUI.View.LeafContent
{
    /// <summary>
    /// SingleSelectView.xaml 的交互逻辑
    /// </summary>
    public partial class SingleSelectView : UserControl
    {
        public SingleSelectView(string hint, object[] options)
        {
            InitializeComponent();
            if (hint != null) TBHint.Text = hint;
            LVOptions.ItemsSource = options;
        }
    }
}
