/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 1:45:06 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.Support.Log;
using System;
using System.Net;

namespace AutumnBox.GUI.ViewModel
{
    class VMEnableDeviceNetDebugging : ViewModelBase
    {
        private const string PORT_HINT_KEY = "ContentEnableDeviceNetDebuggingPortHint";
        private const string PORT_ERR_HINT_KEY = "ContentEnableDeviceNetDebuggingPortErrorHint";
        public Action ViewCloser { get; set; }
        #region MVVM
        public string Hint
        {
            get => _hint; set
            {
                _hint = value;
                RaisePropertyChanged();
            }
        }
        private string _hint;
        public string PortString
        {
            get => _portString; set
            {
                _portString = value;
                RaisePropertyChanged();
            }
        }
        private string _portString = "5555";
        public FlexiableCommand Open { get; private set; }
        #endregion
        public VMEnableDeviceNetDebugging()
        {
            Open = new FlexiableCommand(OpenImpl);
            Hint = App.Current.Resources[PORT_HINT_KEY].ToString();
        }
        private void OpenImpl()
        {
            DeviceBasicInfo target = DeviceSelectionObserver.Instance.CurrentDevice;
            ushort port = 0;
            try
            {
                port = ushort.Parse(PortString);
            }
            catch
            {
                Hint = App.Current.Resources[PORT_ERR_HINT_KEY].ToString();
                return;
            }
            var flow = new NetDebuggingOpener();
            flow.Init(new NetDebuggingOpenerArgs()
            {
                Port = port,
                DevBasicInfo = target
            });
            flow.Finished += (s, e) =>
            {
                var ipAddress = new DeviceSoftwareInfoGetter(target).GetLocationIP();
                ConnectTo(new IPEndPoint(ipAddress, port));
            };
            flow.RunAsync();
            ViewCloser();
        }
        private void ConnectTo(IPEndPoint endPoint)
        {
            try
            {
                var connecter = new NetDeviceConnecter();
                connecter.Init(new NetDeviceConnecterArgs()
                {
                    IPEndPoint = endPoint
                });
                connecter.RunAsync();
            }
            catch (Exception ex)
            {
                Logger.Warn(this, "a exception happend on connect to new device", ex);
            }
        }
    }
}
