using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
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
using System.Windows;

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
                CpuBarStyle = GetProgressBarByValue(value);
                _cpu = value;
                RaisePropertyChanged();
            }
        }
        private double _cpu;

        public Style CpuBarStyle
        {
            get { return cpuBarStyle; }
            set
            {
                cpuBarStyle = value;
                RaisePropertyChanged();
            }
        }
        private Style cpuBarStyle;

        public Style MemBarStyle
        {
            get { return memBarStyle; }
            set
            {
                memBarStyle = value;
                RaisePropertyChanged();
            }
        }
        private Style memBarStyle;

        public string RunningTimeString
        {
            get
            {
                return _runningTime;
            }
            set
            {
                _runningTime = value;
                RaisePropertyChanged();
            }
        }
        private string _runningTime;

        public string BootTimeString
        {
            get
            {
                return _bootTimeString;
            }
            set
            {
                _bootTimeString = value;
                RaisePropertyChanged();
            }
        }
        private string _bootTimeString;

        public double MemPercent
        {
            get
            {
                return _mem;
            }
            set
            {
                MemBarStyle = GetProgressBarByValue(value);
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
        private StatGetter statGetter;
        private const int REFRESH_INTERVAL = 1000;
        public VMDevicePerformance()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    try { Refresh(); }
                    catch (Exception e)
                    {
                        Reset();
                        SLogger<VMDevicePerformance>.Warn("refresh error", e);
                    }
                    Thread.Sleep(REFRESH_INTERVAL);
                }
            });
            DeviceSelectionObserver.Instance.SelectedDevice += (s, e) =>
            {
                IDevice crtDev = DeviceSelectionObserver.Instance.CurrentDevice;
                statGetter = new StatGetter(crtDev, executor);
                getter = new MemInfoGetter(crtDev, executor);
            };
            DeviceSelectionObserver.Instance.SelectedNoDevice += (s, e) =>
            {
                getter = null;
                statGetter = null;
            };
        }
        private void Refresh()
        {
            if (DeviceSelectionObserver.Instance.IsSelectedDevice)
            {
                RefreshMemory();
                RefershCpu();
                RefreshBootTime();
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
        private void RefreshMemory()
        {
            var memInfo = getter.Get();
            UsedMem = Math.Round((memInfo.Total - memInfo.Available) / 1024.0, 1);
            TotalMem = Math.Round(memInfo.Total / 1024.0, 1);
            MemPercent = Math.Round(100.0 * UsedMem / TotalMem, 1);
        }
        private void RefershCpu()
        {
            var image1 = statGetter.GetTotal();
            var image2 = statGetter.GetTotal();
            int s1 = S(image1);
            int s2 = S(image2);
            int total = s2 - s1;
            int idle = image2.Idle - image1.Idle;
            CpuPercent = 100 * (total - idle) / total;
        }
        private void RefreshBootTime()
        {
            var uptime = new Uptime(DeviceSelectionObserver.Instance.CurrentDevice, executor);
            var span = TimeSpan.FromSeconds(uptime.GetRunningSeconds());
            DateTime bootTime = DateTime.Now - span;
            RunningTimeString = $"{(int)span.TotalHours}:{span.Minutes}:{span.Seconds}";
            BootTimeString = string.Format("{0:F}", bootTime);
        }
        private Style GetProgressBarByValue(double value)
        {
            if (value >= 0 && value < 39)
            {
                return App.Current.Resources["ProgressBarSuccess"] as Style;
            }
            else if (value >= 40 && value < 59)
            {
                return App.Current.Resources["ProgressBarInfo"] as Style;
            }
            else if (value >= 60 && value < 79)
            {
                return App.Current.Resources["ProgressBarWarning"] as Style;
            }
            else
            {
                return App.Current.Resources["ProgressBarDanger"] as Style;
            }
        }
        private int S(Stat stat)
        {
            return stat.Guset + stat.Idle + stat.Iowait +
                stat.Irq + stat.Nice + stat.SoftIrq +
                stat.StealStolen + stat.System + stat.User;
        }

    }
}
