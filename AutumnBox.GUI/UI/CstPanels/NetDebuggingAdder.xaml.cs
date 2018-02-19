using AutumnBox.Basic.FlowFramework;
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.UI.FuncPanels;
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
    public partial class NetDebuggingAdder : FastPanelChild
    {
        private NetDeviceConnecter adder = null;
        private DevicesPanel root;
        public NetDebuggingAdder(DevicesPanel root)
        {
            this.root = root;
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
                new FastPanel(this.root.GridMain,
                    new DevicesPanelMessageBox(App.Current.Resources["msgCheckInput"].ToString()))
                    .Display();
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
                Finish();
            }
            else
            {
                new FastPanel(this.root.GridMain, new DevicesPanelMessageBox(App.Current.Resources["msgFailed"].ToString())).Display();
            }
        }

        private const string ipCharPattern = @"\d|\.";
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) { }
        private void TBoxIP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, ipCharPattern);
        }

        private void TBoxPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"\d");
        }

        public override void OnPanelBtnCloseClicked(ref bool prevent)
        {
            base.OnPanelBtnCloseClicked(ref prevent);
            adder?.ForceStop();
        }
    }
}
