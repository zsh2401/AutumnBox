using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMDevicePerformance : ViewModelBase
    {
        public double CpuPercent
        {
            get
            {
                return _cpu;
            }
            set
            {
                _cpu = value;
                RaisePropertyChanged();
            }
        }
        private double _cpu;

        public double MemPercent
        {
            get
            {
                return _mem;
            }
            set
            {
                _mem = value;
                RaisePropertyChanged();
            }
        }
        private double _mem;
        public double TotalMem
        {
            get
            {
                return _totalMem;
            }
            set
            {
                _totalMem = value;
                RaisePropertyChanged();
            }
        }
        private double _totalMem;

        public double UsedMem
        {
            get
            {
                return _usedMem;
            }
            set
            {
                _usedMem = value;
                RaisePropertyChanged();
            }
        }
        private double _usedMem;

        private readonly ICommandExecutor executor = new HestExecutor();
        private MemInfoGetter getter;
        private const int REFRESH_INTERVAL = 1000;
        public VMDevicePerformance()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try { Refresh(); } catch { Reset(); }
                    Thread.Sleep(REFRESH_INTERVAL);
                }
            });
            DeviceSelectionObserver.Instance.SelectedDevice += (s, e) =>
            {
                getter = new MemInfoGetter(DeviceSelectionObserver.Instance.CurrentDevice, executor);
            };
            DeviceSelectionObserver.Instance.SelectedNoDevice += (s, e) =>
            {
                getter = null;
            };
        }
        private void Refresh()
        {
            if (DeviceSelectionObserver.Instance.IsSelectedDevice)
            {
                var memInfo = getter.Get();
                UsedMem =Math.Round(  (memInfo.Total - memInfo.Available) / 1024.0,1);
                TotalMem = Math.Round(memInfo.Total / 1024.0, 1);
                MemPercent = Math.Round(100.0 * UsedMem / TotalMem, 1);
            }
            else
            {
                Reset();
            }
        }
        private void Reset()
        {
            MemPercent = 0;
            CpuPercent = 0;
            TotalMem = 0;
            UsedMem = 0;
        }
    }
}
