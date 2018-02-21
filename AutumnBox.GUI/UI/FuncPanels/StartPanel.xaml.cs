using AutumnBox.GUI.I18N;
using AutumnBox.GUI.NetUtil;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
    public partial class StartPanel : UserControl, ILogSender
    {
        private WebBrowser wb = new WebBrowser();
        public StartPanel()
        {
            InitializeComponent();
            wb.Navigated += (s, e) =>
            {
                try {
                    if ((e.WebResponse as HttpWebResponse).StatusCode == HttpStatusCode.OK)
                    {
                        GridContainer.Children.Add(wb);
                    }
                    else {
                        wb.Dispose();
                    }
                } catch {
                    wb.Dispose();
                } 
            };
            wb.Navigating += (s, e) =>
            {
                if (!e.Uri.ToString().Contains("/api/start/"))
                {
                    Process.Start(e.Uri.ToString());
                }
            };
            if (LanguageHelper.SystemLanguageIsChinese)
            {
                wb.Navigate("http://atmb.top/api/start/zh-CN");
            }
            else
            {
                wb.Navigate("http://atmb.top/api/start/en-US");
            }
        }

        public string LogTag => "WB";

        public bool IsShowLog => true;

        private void TBOfficialWebsite_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(Urls.OFFICIAL_WEBSITE);
        }
    }
}
