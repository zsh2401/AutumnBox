using AutumnBox.Helper;
using System.Windows;
using System.Windows.Input;

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

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void imageClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imageClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.imageClose.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_selected);
        }

        private void imageClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imageClose.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_normal);
        }
    }
}
