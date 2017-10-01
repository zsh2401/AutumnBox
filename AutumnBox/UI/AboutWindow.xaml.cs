using AutumnBox.Helper;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.UI
{
    /// <summary>
    /// AboutWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow(Window owner)
        {
            Owner = owner;
            InitializeComponent();
            this.labelCDate.Content += " " + StaticData.nowVersion.time.ToString("y-M-dd");
        }

        private void labelTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://github.com/zsh2401/AutumnBox/");
        }

        private void imageClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void imageClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.imageClose.Source = UIHelper.BitmapToBitmapImage(AutumnBox.Res.DynamicIcons.close_selected);
        }

        private void imageClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.imageClose.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_normal);
        }
    }
}
