using AutumnBox.GUI.Model;
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
using System.Windows.Shapes;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// MainWindowV2.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindowV2 : Window
    {
        public MainWindowV2()
        {
            InitializeComponent();
        }

        private void ListBox_Selected(object sender, RoutedEventArgs e)
        {
           
        }

        private void ViewItemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IAtmbViewItem selected = (IAtmbViewItem)ViewItemList.SelectedItem;
            ViewGrid.Children.Clear();
            ViewGrid.Children.Add(selected.GetView());
        }
    }
}
