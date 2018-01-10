using AutumnBox.Basic.FlowFramework;
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
    public partial class NetDebuggingAdder : UserControl, ICommunicableWithFastGrid
    {
        private NetDeviceConnecter adder = null;
        public event EventHandler CallFatherToClose;
        public NetDebuggingAdder()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            NetDeviceConnecterArgs args = null;
            try
            {
                IPAddress ip = IPAddress.Parse(TBoxIP.Text.ToString());
                args = new NetDeviceConnecterArgs() { IPEndPoint = new IPEndPoint(ip, int.Parse(TBoxPort.Text)) };
            }
            catch (Exception ex)
            {
                Logger.D("parse textbox input error", ex);
                new FastGrid(this.GridMain, new DevicesPanelMessageBox(App.Current.Resources["msgCheckInput"].ToString()));
                return;
            }
            adder = new NetDeviceConnecter();
            adder.Init(args);
            BtnAdd.Content = App.Current.Resources["btnConnecting"];
            var result = await Task.Run(() =>
            {
                var r = adder.Run();
                return r;
            });
            BtnAdd.Content = App.Current.Resources["btnConnect"];
            if (result.ResultType == ResultType.Successful)
            {
                CallFatherToClose?.Invoke(this, new EventArgs());
            }
            else
            {
                new FastGrid(this.GridMain, new DevicesPanelMessageBox(App.Current.Resources["msgFailed"].ToString()));
            }
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
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //TBStatus.Text = "...";
        }

        public void OnFatherClosed()
        {
            adder?.ForceStop();
        }
    }
}
