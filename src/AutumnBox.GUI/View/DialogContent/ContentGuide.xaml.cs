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
using System.Windows.Navigation;
using System.Windows.Shapes;
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util.Bus;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentGuide.xaml 的交互逻辑
    /// </summary>
    public partial class ContentGuide : UserControl, ISubWindowDialog
    {
        public ContentGuide()
        {
            InitializeComponent();
        }

        public object View => this;

        public event EventHandler<SubWindowFinishedEventArgs> Finished;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(false));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Finished?.Invoke(this, new SubWindowFinishedEventArgs(true));
        }
    }
}
