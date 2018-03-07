using AutumnBox.Basic.Device;
using AutumnBox.Basic.Flows;
using AutumnBox.GUI.UI.Fp;
using AutumnBox.GUI.UI.FuncPanels;
using AutumnBox.Support.Log;
using System;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.UI.CstPanels
{
    /// <summary>
    /// OpenNetDebugging.xaml 的交互逻辑
    /// </summary>
    public partial class OpenNetDebugging : FastPanelChild
    {
        private readonly DeviceSerialNumber _serial;
        private DevicesPanel root;
        public OpenNetDebugging(DevicesPanel root,DeviceSerialNumber serial)
        {
            InitializeComponent();
            this.root = root;
            _serial = serial;
        }

        private const string portPattern = @"\d";
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, portPattern);
        }
        private async void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            //检查输入的端口是否正确
            int port = int.Parse(TBoxPort.Text);
            if (port > 65535)//如果端口号不对
            {//告诉用户不对
                new FastPanel(this.root.GridMain,
                    new DevicesPanelMessageBox(
                        App.Current.Resources["msgPleaseInputAPort"].ToString()
                        )
                ).Display();
                //并且将端口输入框重置
                TBoxPort.Text = "5555";
                //离开当前方法
                return;
            }
            //如果进行到这里,说明检查通过了
            var opener = new NetDebuggingOpener();
            opener.Init(new NetDebuggingOpenerArgs()
            {
                DevBasicInfo = new DeviceBasicInfo()
                {
                    Serial = _serial,
                    State = DeviceState.Poweron,
                },
                Port = (uint)port
            });
            //异步开启该设备的网络调试
            var result = await Task.Run(() =>
            {
                return opener.Run();
            });
            //如果开启成功了
            if (result.ResultType == Basic.FlowFramework.ResultType.Successful)
            {
                try
                {
                    //尝试连接到刚才开启调试的设备
                    await Task.Run(() =>
                    {
                        Thread.Sleep(3000);
                        var IP = new DeviceSoftwareInfoGetter(_serial).GetLocationIP();
                        if (IP != null)
                        {
                            this.Dispatcher.Invoke(() =>
                            {
                                new FastPanel(root.GridMain,
                                             new DevicesPanelMessageBox(
                                             App.Current.Resources["msgGettedIP"].ToString() + Environment.NewLine
                                             + IP.ToString() + ":" + port
                                             )
                                ).Display();
                            });
                            var connecter = new NetDeviceConnecter();
                            connecter.Init(new NetDeviceConnecterArgs()
                            {
                                IPEndPoint = new IPEndPoint(IP, port)
                            });
                            connecter.Run();
                        }
                    });
                }
                catch (Exception ex)
                {
                    Logger.Warn(this,"auto connect failed....", ex);
                }
            }
            //无论如何,执行完后,都要关闭连接界面
            await Task.Run(() => Thread.Sleep(10000));
            Finish();
        }
    }
}
