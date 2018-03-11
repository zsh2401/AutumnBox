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
        public string Title
        {
            get
            {
                return LbTitle.Content as string;
            }
            set
            {
                LbTitle.Content = value;
            }
        }
        private Window _ownerWindow;
        private ITitleBarWindow _ownerWindow_t;
        public CstmTitleBar()
        {
            InitializeComponent();
        }

        private void LoadParentWindow(DependencyObject control)
        {
            var parent = VisualTreeHelper.GetParent(control);
            if (parent == null)
            {
                return;
            }
            else if (parent is ITitleBarWindow)
            {
                this._ownerWindow_t = (ITitleBarWindow)parent;
                return;
            }
            else if (parent is Window)
            {
                this._ownerWindow = (Window)parent;
                return;
            }
            else
            {
                LoadParentWindow(parent);
            };
        }

        private void ImgClose_MouseEnter(object sender, MouseEventArgs e) =>
            ImgClose.Source = ImageGetter.Get("Btn/close_selected.png");

        private void ImgClose_MouseLeave(object sender, MouseEventArgs e) =>
            ImgClose.Source = ImageGetter.Get("Btn/close_normal.png");

        private void ImgMin_MouseEnter(object sender, MouseEventArgs e) =>
            ImgMin.Source = ImageGetter.Get("Btn/min_selected.png");

        private void ImgMin_MouseLeave(object sender, MouseEventArgs e) =>
            ImgMin.Source = ImageGetter.Get("Btn/min_normal.png");

        private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ownerWindow_t?.OnBtnCloseClicked();
            _ownerWindow?.Close();
        }

        private void ImgMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ownerWindow_t?.OnBtnMinClicked();
            if (_ownerWindow != null)
            {
                _ownerWindow.WindowState = WindowState.Minimized;
            }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    _ownerWindow_t?.OnDragMove();
                    _ownerWindow?.DragMove();
                }
                catch (InvalidOperationException) { }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadParentWindow(this);
            ImgMin.Visibility = _ownerWindow_t?.BtnMinEnable == true ? Visibility.Visible : ImgMin.Visibility;
        }
    }
}