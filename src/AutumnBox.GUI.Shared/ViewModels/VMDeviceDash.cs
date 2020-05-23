using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.ManagementV2.OS;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;

using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModels
{
    class VMDeviceDash : ViewModelBase
    {
        public const int REFRESH_INTERVAL = 1 * 1000;
        private readonly ICommandExecutor executor = new HestExecutor();

        public double CpuUsed
        {
            get
            {
                return _cpuUsed;
            }
            set
            {
                _cpuUsed = value;
                RaisePropertyChanged();
            }
        }
        private double _cpuUsed;

        public double MemPercent
        {
            get
            {
                return _mem;
            }
            set
            {
                //MemBarStyle = GetProgressBarByValue(value);
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

        public string StateString
        {
            get
            {
                return _stateString;
            }
            set
            {
                _stateString = value;
                RaisePropertyChanged();
            }
        }
        private string _stateString;

        public string Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
                RaisePropertyChanged();
            }
        }
        private string _model;

        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
                RaisePropertyChanged();
            }
        }
        private string _brand;

        public string Code
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
                RaisePropertyChanged();
            }
        }
        private string _code;

        public string AndroidVersion
        {
            get
            {
                return _androidVersion;
            }
            set
            {
                _androidVersion = value;
                RaisePropertyChanged();
            }
        }
        private string _androidVersion;

        public int BatteryLevel
        {
            get
            {
                return _batterLevel;
            }
            set
            {
                _batterLevel = value;
                RaisePropertyChanged();
            }
        }
        private int _batterLevel;

        public double BatteryVoltage
        {
            get
            {
                return _batteryVoltage;
            }
            set
            {
                _batteryVoltage = value;
                RaisePropertyChanged();
            }
        }
        private double _batteryVoltage;

        public double BatteryTemperature
        {
            get
            {
                return _batteryTemperature;
            }
            set
            {
                _batteryTemperature = value;
                RaisePropertyChanged();
            }
        }
        private double _batteryTemperature;

        public string Charging
        {
            get
            {
                return _charging;
            }
            set
            {
                _charging = value;
                RaisePropertyChanged();
            }
        }
        private string _charging;

        public string LastReboot
        {
            get
            {
                return _lastReboot;
            }
            set
            {
                _lastReboot = value;
                RaisePropertyChanged();
            }
        }
        private string _lastReboot;

        public string BootTime
        {
            get
            {
                return _bootTime;
            }
            set
            {
                _bootTime = value;
                RaisePropertyChanged();
            }
        }
        private string _bootTime;

        public int CpuCoreNumber
        {
            get
            {
                return _cpuCoreNumber;
            }
            set
            {
                _cpuCoreNumber = value;
                RaisePropertyChanged();
            }
        }
        private int _cpuCoreNumber;

        public string ScreenPixel
        {
            get
            {
                return _screenPixel;
            }
            set
            {
                _screenPixel = value;
                RaisePropertyChanged();
            }
        }
        private string _screenPixel;

        public IDevice Device
        {
            get
            {
                return _dev;
            }
            set
            {
                _dev = value;
                RaisePropertyChanged();
            }
        }
        private IDevice _dev;

        private uint refreshCount = 0;

        [AutoInject]
        private readonly IAdbDevicesManager devicesManager;

        public VMDeviceDash()
        {
            Task.Run(() =>
            {
                Thread.CurrentThread.Name = "Device Dash Refresh Thread";
                while (true)
                {
                    Refresh();
                    Thread.Sleep(REFRESH_INTERVAL);
                }
            });
            devicesManager.DeviceSelectionChanged += (s, e) => Reset();
        }
        private void Refresh()
        {
            if (refreshCount > uint.MaxValue - 10) refreshCount = 0;
            refreshCount++;
            if (devicesManager.SelectedDevice != null)
            {
                Device = devicesManager.SelectedDevice;
                try { RefreshStateString(); } catch (Exception e) { SLogger<VMDeviceDash>.Warn("error", e); }
                try { RefreshCpu(); } catch (Exception e) { SLogger<VMDeviceDash>.Warn("error", e); }
                try { RefreshRam(); } catch (Exception e) { SLogger<VMDeviceDash>.Warn("error", e); }
                try { RefreshBuild(); } catch (Exception e) { SLogger<VMDeviceDash>.Warn("error", e); }
                try { RefreshBattery(); } catch (Exception e) { SLogger<VMDeviceDash>.Warn("error", e); }
                try { RefreshBootTime(); } catch (Exception e) { SLogger<VMDeviceDash>.Warn("error", e); }
            }
            else
            {
                Device = null;
                Reset();
            }
        }
        private void RefreshCpu()
        {
            var statGetter = new StatGetter(Device, executor);
            var image1 = statGetter.GetTotal();
            var image2 = statGetter.GetTotal();
            int s1 = image1.Guset + image1.Idle + image1.Iowait +
                image1.Irq + image1.Nice + image1.SoftIrq +
                image1.StealStolen + image1.System + image1.User;
            int s2 = image2.Guset + image2.Idle + image2.Iowait +
                image2.Irq + image2.Nice + image2.SoftIrq +
                image2.StealStolen + image2.System + image2.User;
            int total = s2 - s1;
            int idle = image2.Idle - image1.Idle;
            CpuUsed = 100 * (total - idle) / total;
            CpuCoreNumber = statGetter.GetCpuStats().Count();
        }
        private void RefreshRam()
        {
            var memInfo = new MemInfoGetter(Device, executor).Get();
            UsedMem = Math.Round((memInfo.Total - memInfo.Available) / 1024.0, 1);
            TotalMem = Math.Round(memInfo.Total / 1024.0, 1);
            MemPercent = Math.Round(100.0 * UsedMem / TotalMem, 1);
        }
        private void RefreshBattery()
        {
            if (refreshCount % 3 != 0) return;
            var batteryManager = new BatteryManager(Device, executor);
            var info = batteryManager.GetBatteryInfo();
            BatteryLevel = info.Level ?? 0;
            BatteryVoltage = (info.Voltage ?? 0) / 1000.0;
            Charging = info.Status == 2 ? "√" : "X";
            BatteryTemperature = (info.Temperature ?? 0) / 10.0;
        }
        private void RefreshStateString()
        {
            StateString = App.Current.Resources[$"Dash.State.{Device.State}"].ToString();
        }
        private void RefreshBuild()
        {
            if (refreshCount % 5 != 0) return;
            var buildReader = new CachedBuildReader(Device, executor, true);
            Brand = buildReader.Get(BuildKeys.Brand);
            Model = buildReader.Get(BuildKeys.Model);
            AndroidVersion = buildReader.Get(BuildKeys.AndroidVersion);
            Code = buildReader.Get(BuildKeys.ProductName);
            var wm = new WindowManager(Device, executor);
            var size = wm.Size;
            ScreenPixel = $"{size.Height}x{size.Width}";
        }
        private void RefreshBootTime()
        {
            if (refreshCount % 3 != 0) return;
            var uptime = new Uptime(Device, executor);
            var span = TimeSpan.FromSeconds(uptime.GetRunningSeconds());
            var lastboot = DateTime.Now - span;
            LastReboot = string.Format("{0:F}", lastboot);
            BootTime = $"{(int)span.TotalHours}:{span.Minutes}:{span.Seconds}";
        }
        private void Reset()
        {
            StateString = App.Current.Resources["Dash.State.NoDevice"].ToString();
            CpuUsed = 0;
            BatteryLevel = 0;
            CpuCoreNumber = 0;
            BatteryVoltage = 0;
            TotalMem = 0;
            BatteryTemperature = 0;
            UsedMem = 0;
            MemPercent = 0;
            Charging = "-";
            ScreenPixel = "-";
            AndroidVersion = "-";
            Code = "-";
            Brand = "-";
            BootTime = "-";
            LastReboot = "-";
            Model = "-";
        }
    }
}
