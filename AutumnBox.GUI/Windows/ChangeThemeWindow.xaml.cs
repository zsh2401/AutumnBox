/* =============================================================================*\
*
* Filename: ChangeThemeWindow.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 9/15/2017 15:30:20(UTC+8:00)
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
    /// ChangeThemeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeThemeWindow : Window
    {
        public ChangeThemeWindow()
        {
            InitializeComponent();
            Owner = App.OwnerWindow;
            TitleBar.OwnerWindow = this;
        }
    }
}
