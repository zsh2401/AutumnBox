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
using AutumnBox.GUI.Util.UI;
using System.Windows;

namespace AutumnBox.GUI.Windows
{
    public struct MMessageBoxData
    {
        public string KeyTitle { get; set; }
        public string KeyText { get; set; }
    }
    /// <summary>
    /// MMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class MMessageBox : Window
    {
        public MMessageBoxData Data { get; set; }
        public MMessageBox(Window owner = null)
        {
            InitializeComponent();
            if (owner != null)
                Owner = owner;
            else
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        public void ShowDialog(string title, string message)
        {
            LabelTitle.Content = title;
            TBText.Text = message;
            base.ShowDialog();
        }
        public new void ShowDialog()
        {
            try
            {
                LabelTitle.Content = UIHelper.GetString(Data.KeyTitle ?? "msgNotice");
            }
            catch
            {
                LabelTitle.Content = Data.KeyTitle;
            }
            try { TBText.Text = UIHelper.GetString(Data.KeyText ?? "WTF?"); }
            catch
            {
                TBText.Text = Data.KeyText;
            }
            base.ShowDialog();
        }
    }
}
