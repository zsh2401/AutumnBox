using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutumnBox.GUI.UI.FuncPanels
{
    /// <summary>
    /// StartPanel.xaml 的交互逻辑
    /// </summary>
    public partial class StartPanel : UserControl,ILogSender
    {
        public StartPanel()
        {
            InitializeComponent();
            //WebBrowser.Navigating += (s, e) =>
            //{
            //    Debug.WriteLine(e.WebRequest.RequestUri.ToString());
            //    bool isInnerWeb = e.WebRequest.RequestUri.ToString().Contains("autumnbox_inner");
            //    if (!isInnerWeb)
            //    {
            //        e.Cancel = true;
            //        Process.Start(e.WebRequest.RequestUri.AbsoluteUri);
            //    }
            //};
            WebBrowser.Navigate(new Uri("http://atmb.top"));

        }

        public string LogTag => "WB";

        public bool IsShowLog => true;
    }
}
