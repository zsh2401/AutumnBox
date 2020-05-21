using System.Windows;

namespace AutumnBox.GUI.View.Windows
{
    /// <summary>
    /// FatalWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FatalWindow : Window
    {
        public FatalWindow(string expMsg)
        {
            InitializeComponent();
            TxtBxStackTrace.Text = expMsg;
        }

        private void BtnCopyErrMsg_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TxtBxStackTrace.Text);
            BtnCopyErrMsg.Content = "已复制";
        }
    }
}
