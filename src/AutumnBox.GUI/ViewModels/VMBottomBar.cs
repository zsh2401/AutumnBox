using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.GUI.Services;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.Basic;

namespace AutumnBox.GUI.ViewModels
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

        [AutoInject]
        private readonly IExtensionTaskManager taskManager;

        [AutoInject]
        private readonly IAdbDevicesManager devicesManager;

        public VMBottomBar()
        {
            if (IsDesignMode()) return;
            Port = (ushort)BasicBooter.ServerEndPoint.Port;
            devicesManager.ConnectedDevicesChanged += (s, e) =>
            {
                CountOfDevices = e.Devices?.Count() ?? 0;
            };
            _isAdmin = Self.HaveAdminPermission;
            GetAdbVersion();
            Task.Run(() =>
            {
                Thread.CurrentThread.Name = "Status Bar Information Update Thread";
                while (true)
                {
                    UpdateCountOfTaskRunning();
                    Thread.Sleep(500);
                }
            });
        }

        private void GetAdbVersion()
        {
            using var cmd = BasicBooter.CommandProcedureManager.OpenCommand("version");
            string versionOutput = cmd.Execute().Output;
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
                CountOfTaskRunning = taskManager?.RunningTasks?.Count() ?? 0;
            }
            catch { }
        }
    }
}
