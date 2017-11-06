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
    /// FastBrowser.xaml 的交互逻辑
    /// </summary>
    public partial class FastBrowser : Window
    {
        public FastBrowser(string url = "https://www.baidu.com")
        {
            InitializeComponent();
            this.Owner = App.OwnerWindow;
            TitleBar.OwnerWindow = this;
            MainBrowser.Navigate(new Uri(url));
        }
    }
}
