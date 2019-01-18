using AutumnBox.GUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// LeafWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeafWindow : Window
    {
        public LeafWindow()
        {
            InitializeComponent();
            (DataContext as VMLeafUI).View = this;
        }

        private void TBOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).ScrollToEnd();
        }
        //~LeafWindow() {
        //    Trace.WriteLine("Leaf window ~");
        //}
    }
}
