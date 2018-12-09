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
    /// AdbFailedWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AdbFailedWindow : Window
    {
        public AdbFailedWindow(string message)
        {
            InitializeComponent();
            this.Closed += AdbFailedWindow_Closed;
            tbOutput.Text = message;
        }

        private void AdbFailedWindow_Closed(object sender, EventArgs e)
        {
            App.Current.Shutdown(1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clipboard.SetText(tbOutput.Text);
            }
            catch { }
        }
    }
}
