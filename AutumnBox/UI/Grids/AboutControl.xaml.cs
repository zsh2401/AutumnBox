using AutumnBox.Windows;
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

namespace AutumnBox.UI.Grids
{
    /// <summary>
    /// AboutControl.xaml 的交互逻辑
    /// </summary>
    public partial class AboutControl : UserControl
    {
        public AboutControl()
        {
            InitializeComponent();
            LabelCompiledDate.Content = DebugInfo.CompiledDate.ToString("MM-dd-yyyy");
        }

        private void TextBlockGoToOS_MouseDown(object sender, MouseButtonEventArgs e) =>
            Process.Start(App.Current.Resources["linkAutumnBoxOS"].ToString());

        private void TextBlockDonate_MouseDown(object sender, MouseButtonEventArgs e) =>
            new DonateWindow().ShowDialog();
    }
}
