/* =============================================================================*\
*
* Filename: MMessageBox.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 8/1/2017 12:49:17(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Helper;
using System;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.Windows
{
    /// <summary>
    /// MMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MMessageBox : Window
    {
        public MMessageBox()
        {
            InitializeComponent();
            this.Topmost = true;
        }
        public static void ShowDialog( string title, string content)
        {
            MMessageBox m = new MMessageBox();
            m.textBlockContent.Text = content;
            m.labelTitle.Content = title;
            m.Owner = App.OwnerWindow;
            m.ShowDialog();
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
