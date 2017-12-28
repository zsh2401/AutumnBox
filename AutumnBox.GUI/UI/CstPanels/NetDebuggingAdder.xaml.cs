using AutumnBox.Basic.Flows;
using AutumnBox.Support.CstmDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
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

namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// NetDebuggingAdder.xaml 的交互逻辑
    /// </summary>
    public partial class NetDebuggingAdder : UserControl/*, ICloseNoticeReceivable*/
    {
        private NetDeviceAdder adder;
        public NetDebuggingAdder()
        {
            InitializeComponent();
        }

        public void FatherOnClosing()
        {
            adder?.ForceStop();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            NetDeviceAdderArgs args = null;
            try
            {
                IPAddress ip = IPAddress.Parse(TBoxIP.ToString());
                args = new NetDeviceAdderArgs() { IPEndPoint = new IPEndPoint(ip, int.Parse(TBoxPort.Text)) };
            }
            catch (Exception)
            {
                TBStatus.Text = "Please Check you input";
                return;
            }
            adder = new NetDeviceAdder();
            adder.Init(args);
            BtnAdd.Content = "Adding...";
            var result = await Task.Run(() =>
            {
                var r = adder.Run();
                return r;
            });
            BtnAdd.Content = App.Current.Resources["btnAddNetdebuggingDevice"];
            TBStatus.Text = result.ExitCode == 0 ? "Added..." : "Failed";
        }

        private const string ipCharPattern = @"\d|\.";
        private void TBoxIP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, ipCharPattern);
        }

        private void TBoxPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }

        private void TBoxIP_TextChanged(object sender, TextChangedEventArgs e)
        {
            TBStatus.Text = "...";
        }

        private void TBoxPort_TextChanged(object sender, TextChangedEventArgs e)
        {
            TBStatus.Text = "...";
        }
    }
}
