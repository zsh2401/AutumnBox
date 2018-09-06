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
        private const string DEFAULT_VALUE = "-";
        #region MVVM
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
        private void ResetStateStringByCurrentDevice() {
            StateString = App.Current.Resources["PanelDeviceDetailsState" + DeviceSelectionObserver.Instance.CurrentDevice.State].ToString();
        }

        private void SelectedNoDevice(object sender, System.EventArgs e)
        {
            InfoPanelVisibility = Visibility.Collapsed;
            TranSelectIndex = 0;
            Reset();
        }

        private void SelectedDevice(object sender, System.EventArgs e)
        {
            InfoPanelVisibility = Visibility.Visible;
            TranSelectIndex = 1;
            try
            {
                By(DeviceSelectionObserver.Instance.CurrentDevice);
            }
            catch { }
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
            Brand = buildProp[BuildPropKeys.Brand];
            Model = buildProp[BuildPropKeys.Model];
            AndroidVersion = buildProp[BuildPropKeys.AndroidVersion];
            Product = buildProp[BuildPropKeys.ProductName];
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
        }

        ~VMDeviceDetails()
        {
            DeviceSelectionObserver.Instance.SelectedDevice -= SelectedDevice;
            DeviceSelectionObserver.Instance.SelectedNoDevice -= SelectedNoDevice;
        }
    }
}
