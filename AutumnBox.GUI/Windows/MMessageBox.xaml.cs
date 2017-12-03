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
using System;
using System.Windows;

namespace AutumnBox.GUI.Windows
{
    [Obsolete("please use GUI.UI.Cstm.MessageBlock to instead")]
    /// <summary>
    /// MMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MMessageBox : Window
    {
        public MMessageBox()
        {
            InitializeComponent();
            this.Topmost = true;
            TitleBar.OwnerWindow = this;
        }
        public static void FastShow(string title, string content)
        {
            MMessageBox m = new MMessageBox();
            m.textBlockContent.Text = content;
            m.labelTitle.Content = title;
            m.ShowDialog();
        }
        public static void FastShow(Window ownerWindow, string title, string content)
        {
            MMessageBox m = new MMessageBox();
            m.textBlockContent.Text = content;
            m.labelTitle.Content = title;
            m.Owner = ownerWindow;
            m.ShowDialog();
        }
    }
}
