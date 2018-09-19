/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/21 21:50:07 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Calling.Adb;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Debugging;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Media;

namespace AutumnBox.GUI.ViewModel
{
    class VMContentConnectNetDevice : ViewModelBase
    {
        public const int ADB_NET_DEBUGGING_DEFAULT_PORT = 5555;
        #region MVVM
        public string PortString
        {
            get => _portString; set
            {
                _portString = value;
                RaisePropertyChanged();
            }
        }
        private string _portString = ADB_NET_DEBUGGING_DEFAULT_PORT.ToString();

        public string StateString
        {
            get => _stateString; set
            {
                _stateString = value;
                RaisePropertyChanged();
            }
        }
        private string _stateString = null;

        public Color StateStringColor
        {
            get => _stateStringColor; set
            {
                _stateStringColor = value;
                RaisePropertyChanged();
            }
        }
        private Color _stateStringColor = Colors.Black;

        public string IPString
        {
            get => _ipString; set
            {
                _ipString = value;
                RaisePropertyChanged();
            }
        }
        private string _ipString;

        public FlexiableCommand DoConnect
        {
            get
            {
                return _doConnect;
            }
            set
            {
                _doConnect = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _doConnect;
        #endregion
        public Action OnCloseCallback { get; set; }
        public bool IsRunning { get; set; }

        public VMContentConnectNetDevice()
        {
            DoConnect = new FlexiableCommand(DoConnectImp);
        }
        private void DoConnectImp(object para)
        {
            if (IPString == "iloveyou")
            {
                //我喜欢你,曹娜(*^▽^*)
                Process.Start("http://lovecaona.cn");
                return;
            }
            var input = ParseInput();
            if (!input.Item1) return;
            ConnectTo(input.Item2);
        }
        /// <summary>
        /// 连接到....
        /// </summary>
        /// <param name="ip"></param>
        private void ConnectTo(IPEndPoint ip)
        {
            StateString = App.Current.Resources["ContentConnectNetDeviceStateStringConnecting"].ToString();
            DoConnect.CanExecuteProp = false;
            IsRunning = true;
            var result = new AdbCommand($"{ip.Address}:{ip.Port}").To(
                 (e) =>
                 {
                     SGLogger<VMContentConnectNetDevice>.Info("connecting:" + e.Text);
                 }
                 ).Execute();
            IsRunning = false;
            if (result.ExitCode == 0)
            {
                OnCloseCallback();
            }
            else
            {
                StateString = App.Current.Resources["ContentConnectNetDeviceStateStringFailed"].ToString();
                DoConnect.CanExecuteProp = true;
            }
        }
        private Tuple<bool, IPEndPoint> ParseInput()
        {
            ushort port = 0;
            IPAddress ipAddress = null;
            try
            {
                ipAddress = IPAddress.Parse(IPString);
            }
            catch
            {
                StateString = App.Current.Resources["ContentConnectNetDeviceStateStringIPWrong"].ToString();
                return new Tuple<bool, IPEndPoint>(false, null);
            }
            try
            {
                port = ushort.Parse(PortString);
            }
            catch
            {
                StateString = App.Current.Resources["ContentConnectNetDeviceStateStringPortWrong"].ToString();
                return new Tuple<bool, IPEndPoint>(false, null);
            }
            return new Tuple<bool, IPEndPoint>(true, new IPEndPoint(ipAddress, port));
        }
    }
}
