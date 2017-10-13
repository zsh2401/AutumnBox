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
using System.Windows.Shapes;

namespace AutumnBox.Windows
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
        }
    }
}
