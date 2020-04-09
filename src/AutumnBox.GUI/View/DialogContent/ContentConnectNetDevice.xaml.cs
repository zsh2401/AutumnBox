using AutumnBox.OpenFramework.Open.LKit;
using System;
using System.Net;
using System.Windows.Controls;

namespace AutumnBox.GUI.View.DialogContent
{
    /// <summary>
    /// ContentConnectNetDevice.xaml 的交互逻辑
    /// </summary>
    public partial class ContentConnectNetDevice : UserControl, ILeafDialog
    {
        public ContentConnectNetDevice()
        {
            InitializeComponent();
        }

        public object ViewContent => this;

        public event EventHandler<DialogClosedEventArgs> Closed;

        public class InputResult
        {
            public bool IsInputRight { get; }
            public IPEndPoint Result { get; }
            public InputResult(string ip, string port)
            {
                try
                {
                    var _ip = IPAddress.Parse(ip);
                    Result = new IPEndPoint(_ip, ushort.Parse(port));
                    IsInputRight = true;
                }
                catch
                {
                    IsInputRight = false;
                }
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(new InputResult(TBIP.Text, TBPort.Text)));
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            Closed?.Invoke(this, new DialogClosedEventArgs(null));
        }
    }
}
