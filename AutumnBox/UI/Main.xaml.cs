using AutumnBox.Images.DynamicIcons;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        private string _langname = "zh-cn";
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_langname == "zh-cn")
            {
                Application.Current.Resources.Source = new Uri(@"Lang\en-us.xaml", UriKind.Relative);
                _langname = "en-us";
            }
            else {
                Application.Current.Resources.Source = new Uri(@"Lang\zh-cn.xaml", UriKind.Relative);
                _langname = "zh-cn";
            }
        }

        private void CustomTitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed) {
                this.DragMove();
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }


        private void LabelClose_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.close_normal)
            };
        }

        private void LabelClose_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelClose.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.close_selected)
            };
        }

        private void LabelMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LabelMin_MouseEnter(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.min_selected)
            };
        }

        private void LabelMin_MouseLeave(object sender, MouseEventArgs e)
        {
            this.LabelMin.Background = new ImageBrush
            {
                ImageSource = Tools.BitmapToBitmapImage(DyanamicIcons.min_normal)
            };
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
