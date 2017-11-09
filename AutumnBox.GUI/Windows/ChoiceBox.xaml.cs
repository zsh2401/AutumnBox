/* =============================================================================*\
*
* Filename: ChoiceBox.xaml.cs
* Description: 
*
* Version: 1.0
* Created: 8/4/2017 12:13:27(UTC+8:00)
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

namespace AutumnBox.GUI.Windows
{
    /// <summary>
    /// ChoiceBox.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceBox : Window
    {
        public bool Result;
        private ChoiceBox()
        {
            InitializeComponent();
        }
        public static bool FastShow(string title, string content, string btnOkString = null, string btnCancelString = null) {
            ChoiceBox choiceBox = new ChoiceBox();
            choiceBox.labelTitle.Content = title;
            choiceBox.textBlockContent.Text = content ?? choiceBox.textBlockContent.Text;
            choiceBox.btnCancel.Content = btnCancelString ?? choiceBox.btnCancel.Content;
            choiceBox.btnOk.Content = btnOkString ?? choiceBox.btnOk.Content;
            choiceBox.ShowDialog();
            return choiceBox.Result;
        }
        public static bool FastShow(Window owner, string title, string content, string btnOkString = null, string btnCancelString = null)
        {
            ChoiceBox choiceBox = new ChoiceBox();
            choiceBox.labelTitle.Content = title;
            choiceBox.textBlockContent.Text = content ?? choiceBox.textBlockContent.Text;
            choiceBox.btnCancel.Content = btnCancelString ?? choiceBox.btnCancel.Content;
            choiceBox.btnOk.Content = btnOkString ?? choiceBox.btnOk.Content;
            choiceBox.Owner = owner;
            choiceBox.ShowDialog();
            return choiceBox.Result;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
        }

        private void labelTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
