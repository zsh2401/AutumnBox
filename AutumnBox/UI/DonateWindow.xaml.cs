using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    /// <summary>
    /// DonateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DonateWindow : Window
    {
        public DonateWindow(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            this.imageClose.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.close_selected);
        }

        private void imageClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imageClose.Source = Tools.BitmapToBitmapImage(Res.DynamicIcons.close_normal);
        }
    }
}
