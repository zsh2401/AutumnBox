/*
 更新提示窗口
 别问我为什么叫WTF
上一个更新提示窗口因为SB BUG已经凉了
 */

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

namespace AutumnBox.UI
{
    /// <summary>
    /// WTF.xaml 的交互逻辑
    /// </summary>
    public partial class WTF : Window
    {
        public WTF()
        {
            ShowInTaskbar = false;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
