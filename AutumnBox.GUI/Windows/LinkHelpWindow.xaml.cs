/* =============================================================================*\
*
* Filename: LinkHelpWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 8/19/2017 08:38:22(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.GUI.Helper;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// LinkHelpWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LinkHelpWindow : Window
    {
        public LinkHelpWindow()
        {
            this.Owner = App.Current.MainWindow;
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
    }
}
