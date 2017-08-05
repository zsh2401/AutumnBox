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

namespace AutumnBox.UI
{
    /// <summary>
    /// ContactWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ContactWindow : Window
    {
        public ContactWindow(Window owner)
        {
            this.Owner = owner;
            InitializeComponent();
        }

        private void labelTitle_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }
    }
}
