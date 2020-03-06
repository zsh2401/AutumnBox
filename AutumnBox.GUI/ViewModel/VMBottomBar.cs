using AutumnBox.Basic.Calling.Adb;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.OpenFramework.Management.ExtensionThreading;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMBottomBar : ViewModelBase
    {
        public ushort Port
        {
            get => port; set
            {
                port = value;
                RaisePropertyChanged();
            }
        }
        private ushort port;

        public int CountOfDevices
        {
            get => _countOfDevices; set
            {
                _countOfDevices = value;
                RaisePropertyChanged();
            }
        }
        private int _countOfDevices;

        public bool IsAdmin
        {
            get => _isAdmin; set
            {
                _isAdmin = value;
                RaisePropertyChanged();
            }
        }
        private bool _isAdmin;

        public string AdbVersion
        {
            get => _adbVersion; set
            {
                _adbVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _adbVersion;

        public int CountOfTaskRunning
        {
            get => _countOfTaskRunning; set
            {
                _countOfTaskRunning = value;
                RaisePropertyChanged();
            }
        }
        private int _countOfTaskRunning;

        private readonly IExtensionThreadManager threadManager;

        public VMBottomBar()
        {
            Port = Basic.ManagedAdb.Adb.Server.Port;
            ConnectedDevicesListener.Instance.DevicesChanged += (s, e) =>
            {
                CountOfDevices = e.Devices?.Count() ?? 0;
            };
            _isAdmin = Self.HaveAdminPermission;
            GetAdbVersion();
            threadManager = ExtensionThreadManager.Instance;
            Task.Run(() =>
            {
                while (true)
                {
                    UpdateCountOfTaskRunning();
                    Thread.Sleep(500);
                }
            });
        }

        private void GetAdbVersion()
        {
            string versionOutput = new AdbCommand("version").Execute().Output;
            var match = Regex.Match(versionOutput, @"[\w|\s]*[version\s](?<name>[\d|\.]+)([\r\n|\n]*)Version\s(?<code>\d+)", RegexOptions.Multiline);
            if (match.Success)
            {
                AdbVersion = match.Result("${name}(${code})");
            }
        }

        private void UpdateCountOfTaskRunning()
        {
            try
            {
                CountOfTaskRunning = threadManager.GetRunning().Count();
            }
            catch { }
        }
    }
}
