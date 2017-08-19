using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    /// <summary>
    /// RateBox.xaml 的交互逻辑
    /// </summary>
    public partial class RateBox : Window
    {
        private Thread rateThread;
        public RateBox(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }
        public RateBox(Window owner,Thread t):this(owner)
        {
            this.rateThread = t;
            this.buttonCancel.Visibility = Visibility.Visible;
        }
        public new void ShowDialog()
        {
            base.ShowDialog();
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void buttonStartBrventService_Click(object sender, RoutedEventArgs e)
        {
            this.rateThread.Abort();
            this.Close();
        }
    }
}
