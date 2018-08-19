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
        public static readonly DependencyProperty CloseBtnVisibilityProperty
   = DependencyProperty.Register("CloseBtnVisibility", typeof(Visibility), typeof(CstmTitleBar),
       new UIPropertyMetadata(Visibility.Visible));
        public Visibility CloseBtnVisibility
        {
            get { return (Visibility)GetValue(CloseBtnVisibilityProperty); }
            set { SetValue(CloseBtnVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MinBtnVisibilityProperty
   = DependencyProperty.Register("MinBtnVisibility", typeof(Visibility), typeof(CstmTitleBar),
       new UIPropertyMetadata(Visibility.Hidden));
        public Visibility MinBtnVisibility
        {
            get { return (Visibility)GetValue(MinBtnVisibilityProperty); }
            set { SetValue(MinBtnVisibilityProperty, value); }
        }

        public static readonly DependencyProperty MaxBtnVisibilityProperty
   = DependencyProperty.Register("MaxBtnVisibility", typeof(Visibility), typeof(CstmTitleBar),
       new UIPropertyMetadata(Visibility.Hidden));
        public Visibility MaxBtnVisibility
        {
            get { return (Visibility)GetValue(MaxBtnVisibilityProperty); }
            set { SetValue(MaxBtnVisibilityProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty
   = DependencyProperty.Register("Title", typeof(string), typeof(CstmTitleBar),
       new UIPropertyMetadata("AutumnBox"));
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private Window _ownerWindow;
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
            else if (parent is Window)
            {
                _ownerWindow = (Window)parent;
                return;
            }
            else
            {
                LoadParentWindow(parent);
            };
        }

        //private void ImgClose_MouseEnter(object sender, MouseEventArgs e) =>
        //    ImgClose.Source = ImageGetter.Get("Btn/close_selected.png");

        //private void ImgClose_MouseLeave(object sender, MouseEventArgs e) =>
        //    ImgClose.Source = ImageGetter.Get("Btn/close_normal.png");

        //private void ImgMin_MouseEnter(object sender, MouseEventArgs e) =>
        //    ImgMin.Source = ImageGetter.Get("Btn/min_selected.png");

        //private void ImgMin_MouseLeave(object sender, MouseEventArgs e) =>
        //    ImgMin.Source = ImageGetter.Get("Btn/min_normal.png");

        //private void ImgClose_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    _ownerWindow?.Close();
        //}

        private void ImgMin_MouseDown(object sender, MouseButtonEventArgs e)
        {
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
                    _ownerWindow?.DragMove();
                }
                catch (InvalidOperationException) { }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadParentWindow(this);
        }

        private void ImgMax_MouseEnter(object sender, MouseEventArgs e)
        {
            if (_ownerWindow != null)
            {
                _ownerWindow.WindowState = WindowState.Maximized;
            }
        }

        private void ImgMax_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void ImgMax_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ImageBtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ownerWindow?.Close();
        }

        private void ImageBtn_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (_ownerWindow != null)
            {
                _ownerWindow.WindowState = WindowState.Maximized;
            }
        }

        private void ImageBtn_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (_ownerWindow != null)
            {
                _ownerWindow.WindowState = WindowState.Minimized;
            }
        }
    }
}