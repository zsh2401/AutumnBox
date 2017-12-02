using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Resources.DynamicIcons;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// CstmTitleBar.xaml 的交互逻辑
    /// </summary>
    public partial class CstmTitleBar : Grid
    {
        public Window OwnerWindow { private get; set; }
        public CstmTitleBar()
        {
            InitializeComponent();
        }
        private void ImgClose_MouseEnter(object sender, MouseEventArgs e) =>
            ImgClose.Source = UIHelper.BitmapToBitmapImage(DynamicIcons.close_selected);

        private void ImgClose_MouseLeave(object sender, MouseEventArgs e) =>
            ImgClose.Source = UIHelper.BitmapToBitmapImage(DynamicIcons.close_normal);

        private void ImgMin_MouseEnter(object sender, MouseEventArgs e) =>
            ImgMin.Source = UIHelper.BitmapToBitmapImage(DynamicIcons.min_selected);

        private void ImgMin_MouseLeave(object sender, MouseEventArgs e) =>
            ImgMin.Source = UIHelper.BitmapToBitmapImage(DynamicIcons.min_normal);

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e) =>
            OwnerWindow.Close();
        private void ImgMin_MouseDown(object sender, MouseButtonEventArgs e) =>
            OwnerWindow.WindowState = WindowState.Minimized;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) =>
            UIHelper.DragMove(OwnerWindow, e);
    }
}