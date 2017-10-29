/* =============================================================================*\
*
* Filename: CstmButton.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 10/2/2017 23:08:35(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using System;
using System.Windows;
using System.Windows.Controls;
using AutumnBox.Basic.Devices;

namespace AutumnBox.GUI.UI.Cstm
{
    /// <summary>
    /// CstmButton.xaml 的交互逻辑
    /// </summary>
    public partial class CstmButton : Button
    {
        public static readonly DependencyProperty ContorlNameProperty =
            DependencyProperty.Register("ContorlName", typeof(string), typeof(CstmButton), new PropertyMetadata());
        public CstmButton()
        {
            InitializeComponent();
        }

        public void ChangeByStatus(DeviceStatus nowStatus)
        {
            throw new NotImplementedException();
        }
    }
}
