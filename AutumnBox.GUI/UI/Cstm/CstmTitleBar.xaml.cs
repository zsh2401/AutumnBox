using AutumnBox.GUI.Helper;
using AutumnBox.GUI.Resources.Images;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// CstmTitleBar.xaml 的交互逻辑
    /// </summary>
    public partial class CstmTitleBar : UserControl
    {
        public Window OwnerWindow
        {
            set
            {
                _ownerWindow = value;
            }
        }
        private Window _ownerWindow;
        public CstmTitleBar()
        {
            InitializeComponent();
        }
        private static Window FindParentWindow(DependencyObject control)
        {
            var parent = VisualTreeHelper.GetParent(control);
            if (parent == null)
            {
                return null;
            }
            else if (parent is Window)
            {
                return (Window)parent;
            };
            return FindParentWindow(parent);
        }
        private void ImgClose_MouseEnter(object sender, MouseEventArgs e) =>
            ImgClose.Source = ImageGetter.Get("Btn/close_selected.png");

        private void ImgClose_MouseLeave(object sender, MouseEventArgs e) =>
            ImgClose.Source = ImageGetter.Get("Btn/close_normal.png");

        private void ImgMin_MouseEnter(object sender, MouseEventArgs e) =>
            ImgMin.Source = ImageGetter.Get("Btn/min_selected.png");

        private void ImgMin_MouseLeave(object sender, MouseEventArgs e) =>
            ImgMin.Source = ImageGetter.Get("Btn/min_normal.png");

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e) =>
            _ownerWindow.Close();
        private void ImgMin_MouseDown(object sender, MouseButtonEventArgs e) =>
            _ownerWindow.WindowState = WindowState.Minimized;

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try { _ownerWindow.DragMove(); } catch (InvalidOperationException) { }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this._ownerWindow = FindParentWindow(this);
        }
    }
}