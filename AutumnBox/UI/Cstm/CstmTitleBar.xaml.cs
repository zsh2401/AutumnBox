using AutumnBox.Helper;
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

namespace AutumnBox.UI.Cstm
{
    /// <summary>
    /// CstmTitleBar.xaml 的交互逻辑
    /// </summary>
    public partial class CstmTitleBar : UserControl
    {
        public Window OwnerWindow { private get; set; }
        public CstmTitleBar() => InitializeComponent();
        private void ImgClose_MouseEnter(object sender, MouseEventArgs e) =>
            ImgClose.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_selected);

        private void ImgClose_MouseLeave(object sender, MouseEventArgs e) =>
            ImgClose.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.close_normal);

        private void ImgMin_MouseEnter(object sender, MouseEventArgs e) =>
            ImgMin.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.min_selected);

        private void ImgMin_MouseLeave(object sender, MouseEventArgs e) =>
            ImgMin.Source = UIHelper.BitmapToBitmapImage(Res.DynamicIcons.min_normal);

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e) =>
            OwnerWindow.Close();

        private void ImgMin_MouseDown(object sender, MouseButtonEventArgs e) =>
            OwnerWindow.WindowState = WindowState.Minimized;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) =>
            UIHelper.DragMove(OwnerWindow, e);
    }
}
