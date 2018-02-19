using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.UI.FuncPanels;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// CloseNetDebugging.xaml 的交互逻辑
    /// </summary>
    public partial class CloseNetDebugging : FastPanelChild
    {
        private readonly DeviceSerial _serial;
        private readonly DevicesPanel root;
        public CloseNetDebugging(DevicesPanel root,DeviceSerial serial)
        {
            InitializeComponent();
            _serial = serial;
            TBIP.Text = serial.ToString();
            this.root = root;
        }

        private async void BtnCloseNetDebugging_Click(object sender, RoutedEventArgs e)
        {
            var closer = new NetDebuggingCloser();
            closer.Init(new Basic.FlowFramework.FlowArgs()
            {
                DevBasicInfo = new DeviceBasicInfo()
                {
                    State = DeviceState.Poweron,
                    Serial = _serial
                }
            });
            var result = await Task.Run(() =>
            {
                return closer.Run();
            });
            Finish();
        }

        private async void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            var remover = new NetDeviceDisconnecter();
            remover.Init(new Basic.FlowFramework.FlowArgs()
            {
                DevBasicInfo = new DeviceBasicInfo()
                {
                    State = DeviceState.Poweron,
                    Serial = _serial
                }
            });
            var result = await Task.Run(() =>
            {
                return remover.Run();
            });
            Finish();
        }
    }
}
