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
