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
using AutumnBox.GUI.Helper;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.Windows
{
    public enum ChoiceResult
    {
        BtnRight = 0,
        BtnLeft = 1,
        BtnCancel = -1,
    }
    public struct ChoiceBoxData
    {
        public string KeyTitle { get; set; }
        public string KeyText { get; set; }
        public string KeyBtnLeft { get; set; }
        public string KeyBtnRight { get; set; }
    }
    /// <summary>
    /// ChoiceBox.xaml 的交互逻辑
    /// </summary>
    public partial class ChoiceBox : Window
    {
        public ChoiceBoxData Data { get; set; } = new ChoiceBoxData();

        private ChoiceResult _result = ChoiceResult.BtnCancel;

        internal ChoiceBox(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
        }

        public new ChoiceResult ShowDialog()
        {
            LabelTitle.Content = UIHelper.GetString(Data.KeyTitle ?? "msgNotice");
            TBContent.Text = UIHelper.GetString(Data.KeyText ?? "WTF?");
            BtnLeft.Content = UIHelper.GetString(Data.KeyBtnLeft ?? "btnCancel");
            BtnRight.Content = UIHelper.GetString(Data.KeyBtnRight ?? "btnRight");
            base.ShowDialog();
            return _result;
        }

        private void ImgCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _result = ChoiceResult.BtnCancel;
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            UIHelper.DragMove(this, e);
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            _result = ChoiceResult.BtnRight;
            Close();
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            _result = ChoiceResult.BtnLeft;
            Close();
        }
    }
}
