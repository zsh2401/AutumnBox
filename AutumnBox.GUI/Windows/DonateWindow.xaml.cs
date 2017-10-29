/* =============================================================================*\
*
* Filename: DonateWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 8/5/2017 15:48:24(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System.Windows;

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// DonateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DonateWindow : Window
    {
        public DonateWindow()
        {
            InitializeComponent();
            this.Owner = App.OwnerWindow;
            TitleBar.OwnerWindow = this;
            TitleBar.ImgMin.Visibility = Visibility.Hidden;
        }
    }
}
