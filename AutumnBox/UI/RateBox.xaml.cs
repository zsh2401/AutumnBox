using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    /// <summary>
    /// RateBox.xaml 的交互逻辑
    /// </summary>
    public partial class RateBox : Window
    {
        public RateBox()
        {
            InitializeComponent();
            //this.Topmost = 
        }
        public new void ShowDialog() {
                base.ShowDialog();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
