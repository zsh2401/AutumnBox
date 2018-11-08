/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:51:33 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.Device.Management.Hardware;
using AutumnBox.Basic.Device.Management.OS;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.GUI.Util.I18N;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace AutumnBox.GUI.ViewModel
{
    class VMDeviceDetails : ViewModelBase
    {
        protected override bool RaisePropertyChangedOnDispatcher { get; set; } = true;
        private static string TryGet(Dictionary<string, string> dict, string key)
        {
            try
            {
                return dict[key];
            }
            catch { }
            return null;
        }
        private const string DEFAULT_VALUE = "-";
        #region MVVM
        public string StateTip
        {
            get => _stateTip; set
            {
                _stateTip = value;
                RaisePropertyChanged();
            }
        }
        private string _stateTip;

        public Visibility InfoPanelVisibility
        {
            get => infoPanelVisibility; set
            {
                infoPanelVisibility = value;
                RaisePropertyChanged();
            }
        }
        private Visibility infoPanelVisibility;

        public string StateString
        {
            get => stateString; set
            {
                stateString = value;
                RaisePropertyChanged();
            }
        }
        private string stateString;

        public string Brand
        {
            get => brand; set
            {
                brand = value;
                RaisePropertyChanged();
            }
        }
        private string brand;

        public string Density
        {
            get
            {
                return _density;
            }
            set
            {
                _density = value;
                RaisePropertyChanged();
            }
        }
        private string _density;

        public string ScreenSize
        {
            get
            {
                return _screenSize;
            }
            set
            {
                _screenSize = value;
                RaisePropertyChanged();
            }
        }
        private string _screenSize;

        public string Model
        {
            get => model; set
            {
                model = value;
                RaisePropertyChanged();
            }
        }
        private string model;

        public string Product
        {
            get => product; set
            {
                product = value;
                RaisePropertyChanged();
            }
        }
        private string product;

        public string Storage
        {
            get => storage; set
            {
                storage = value;
                RaisePropertyChanged();
            }
        }
        private string storage;

        public string Screen
        {
            get => screen; set
            {
                screen = value;
                RaisePropertyChanged();
            }
        }
        private string screen;

        public string Ram
        {
            get => ram; set
            {
                ram = value;
                RaisePropertyChanged();
            }
        }
        private string ram;

        public string Root
        {
            get => root; set
            {
                root = value;
                RaisePropertyChanged();
            }
        }
        private string root;

        public string AndroidVersion
        {
            get => androidVersion; set
            {
                androidVersion = value;
                RaisePropertyChanged();
            }
        }
        private string androidVersion;

        public int TranSelectIndex
        {
            get => selectIndex; set
            {
                selectIndex = value;
                RaisePropertyChanged();
            }
        }
        private int selectIndex = 0;
        #endregion
        public VMDeviceDetails()
        {
            LanguageManager.Instance.LanguageChanged += AppLanguageChanged;
            DeviceSelectionObserver.Instance.SelectedDevice += SelectedDevice;
            DeviceSelectionObserver.Instance.SelectedNoDevice += SelectedNoDevice;
        }

        private void AppLanguageChanged(object sender, EventArgs e)
        {
            ResetStateStringByCurrentDevice();
        }
        private void ResetStateStringByCurrentDevice()
        {
            if (DeviceSelectionObserver.Instance.CurrentDevice == null)
            {
                StateString = App.Current.Resources["PanelDeviceDetailsStateNotFound"].ToString();
            }
            else
            {
                StateTip = App.Current.Resources["PanelDeviceDetailsStateTip" + DeviceSelectionObserver.Instance.CurrentDevice.State]?.ToString();
                StateString = App.Current.Resources["PanelDeviceDetailsState" + DeviceSelectionObserver.Instance.CurrentDevice.State]?.ToString();
            }

        }

        private void SelectedNoDevice(object sender, EventArgs e)
        {
            InfoPanelVisibility = Visibility.Collapsed;
            TranSelectIndex = 0;
            Reset();
        }

        private void SelectedDevice(object sender, EventArgs e)
        {
            InfoPanelVisibility = Visibility.Visible;
            TranSelectIndex = 1;
            try
            {
                Task.Run(() =>
                {
                    RefreshInformationsThreadMethod(DeviceSelectionObserver.Instance.CurrentDevice);
                });
                //By(DeviceSelectionObserver.Instance.CurrentDevice);
            }
            catch(Exception ex) {
                SLogger.Warn(this,"can't refresh device informations",ex);
            }
        }
        private void RefreshInformationsThreadMethod(IDevice device)
        {
            int currentCode = ran.Next();
            taskCode = currentCode;
            ResetStateStringByCurrentDevice();

            //获取与刷新build.prop内容
            if (currentCode != taskCode) return;
            Dictionary<string, string> buildProp = null;
            var getter = new DeviceBuildPropGetter(device);
            buildProp = getter.GetFull();
            Brand = TryGet(buildProp, BuildPropKeys.Brand);
            Model = TryGet(buildProp, BuildPropKeys.Model);
            AndroidVersion = TryGet(buildProp, BuildPropKeys.AndroidVersion);
            Product = TryGet(buildProp, BuildPropKeys.ProductName);
            Root = device.HaveSU() ? "√" : "X";

            //获取与刷新设备硬件信息
            if (currentCode != taskCode) return;
            DeviceHardwareInfoGetter hwInfoGetter = new DeviceHardwareInfoGetter(device);
            var hwInfo = hwInfoGetter.Get();
            if (currentCode != taskCode) return;
            Screen = hwInfo.ScreenInfo;
            Ram = hwInfo.SizeofRam + "G";
            Storage = hwInfo.SizeofRom + "G";

            //获取于刷新分辨率信息
            if (currentCode != taskCode) return;
            int? density = null;
            int? screenH = null;
            int? screenW = null;
            try
            {
                WindowManager wm = new WindowManager(device);
                density = wm.Density;
                var sz = wm.Size;
                screenH = sz.Height;
                screenW = sz.Width;
            }
            catch (Exception ex)
            {
                SLogger.Warn(this, $"{device}'s window manager error", ex);
            }
            Density = density?.ToString() ?? DEFAULT_VALUE;
            ScreenSize = $"{screenW?.ToString() ?? DEFAULT_VALUE}x{screenH?.ToString() ?? DEFAULT_VALUE}";
        }

        private void Reset()
        {
            ResetStateStringByCurrentDevice();
            Brand = DEFAULT_VALUE;
            AndroidVersion = DEFAULT_VALUE;
            Product = DEFAULT_VALUE;
            Model = DEFAULT_VALUE;
            Root = DEFAULT_VALUE;
            Ram = DEFAULT_VALUE;
            Root = DEFAULT_VALUE;
            Density = DEFAULT_VALUE;
            ScreenSize = DEFAULT_VALUE;
        }

        private Random ran = new Random();
        private int taskCode = 0;

        private async void By(IDevice device)
        {
            int currentCode = ran.Next();
            taskCode = currentCode;
            ResetStateStringByCurrentDevice();
            Dictionary<string, string> buildProp = null;
            await Task.Run(() =>
            {
                var getter = new DeviceBuildPropGetter(device);
                buildProp = getter.GetFull();
            });
            if (currentCode != taskCode) return;
            Brand = TryGet(buildProp, BuildPropKeys.Brand);
            Model = TryGet(buildProp, BuildPropKeys.Model);
            AndroidVersion = TryGet(buildProp, BuildPropKeys.AndroidVersion);
            Product = TryGet(buildProp, BuildPropKeys.ProductName);
            Root = device.HaveSU() ? "√" : "X";

            var hwInfo = await Task.Run(() =>
            {
                var getter = new DeviceHardwareInfoGetter(device);
                return getter.Get();
            });
            if (currentCode != taskCode) return;
            Screen = hwInfo.ScreenInfo;
            Ram = hwInfo.SizeofRam + "G";
            Storage = hwInfo.SizeofRom + "G";
            int? density = null;
            int? screenH = null;
            int? screenW = null;
            await Task.Run(() =>
             {
                 try
                 {
                     WindowManager wm = new WindowManager(device);
                     density = wm.Density;
                     var sz = wm.Size;
                     screenH = sz.Height;
                     screenW = sz.Width;
                 }
                 catch (Exception ex)
                 {
                     SLogger.Warn(this, $"{device}'s window manager error", ex);
                 }
             });
            Density = density?.ToString() ?? DEFAULT_VALUE;
            ScreenSize = $"{screenW?.ToString() ?? DEFAULT_VALUE}x{screenH?.ToString() ?? DEFAULT_VALUE}";
        }

        ~VMDeviceDetails()
        {
            DeviceSelectionObserver.Instance.SelectedDevice -= SelectedDevice;
            DeviceSelectionObserver.Instance.SelectedNoDevice -= SelectedNoDevice;
        }
    }
}
