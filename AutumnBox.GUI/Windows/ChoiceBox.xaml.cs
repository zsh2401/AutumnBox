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
using AutumnBox.GUI.Util.UI;
using System;
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

        internal ChoiceBox()
        {
            InitializeComponent();
            closeAnimationByBtnCancel.Storyboard.Completed += (s, e) => Close();
            closeAnimationByBtnOK.Storyboard.Completed += (s, e) => Close();
            closeAnimationByImgCancel.Storyboard.Completed += (s, e) => Close();
        }

        public new ChoiceResult ShowDialog()
        {
            LabelTitle.Content = UIHelper.GetString(Data.KeyTitle ?? "msgNotice");
            TBContent.Text = UIHelper.GetString(Data.KeyText ?? "WTF?");
            BtnLeft.Content = UIHelper.GetString(Data.KeyBtnLeft ?? "btnCancel");
            BtnRight.Content = UIHelper.GetString(Data.KeyBtnRight ?? "btnContinue");
            try { base.ShowDialog(); } catch { }
            return _result;
        }

        private void ImgCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DialogResult = null;
            _result = ChoiceResult.BtnCancel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                try { this.DragMove(); } catch (Exception) { }
            }
        }

        private void BtnRight_Click(object sender, RoutedEventArgs e)
        {
            _result = ChoiceResult.BtnRight;
            this.DialogResult = true;
        }

        private void BtnLeft_Click(object sender, RoutedEventArgs e)
        {
            _result = ChoiceResult.BtnLeft;
            this.DialogResult = true;
        }
    }
}
