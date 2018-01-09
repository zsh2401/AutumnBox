using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// CloseNetDebugging.xaml 的交互逻辑
    /// </summary>
    public partial class CloseNetDebugging : UserControl, ICommunicableWithFastGrid
    {
        private readonly Serial _serial;

        public CloseNetDebugging(Serial serial)
        {
            InitializeComponent();
            _serial = serial;
            TBIP.Text = serial.ToString();
        }

        public event EventHandler CallFatherToClose;

        public void OnFatherClosed()
        {

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
            CallFatherToClose?.Invoke(this, new EventArgs());
        }

        private async void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            var remover = new NetDeviceRemover();
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
            CallFatherToClose?.Invoke(this, new EventArgs());
        }
    }
}
