using AutumnBox.Basic.Device;
using AutumnBox.GUI.I18N;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox.GUI.UI.FuncPanels
{
    public static class SomeExtension
    {
        public static string AsGbString(this double num)
        {
            return num.ToString() + "GB";
        }
        public static string SafeGet(this Dictionary<string, string> dict, String key)
        {
            try
            {
                return dict[key];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }
    /// <summary>
    /// DeviceInfoPanel.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfoPanel : UserControl, IDeviceRefreshable
    {
        private const string defaultStr = "...";

        public static readonly DependencyProperty DeviceRootStatusTextProperty
            = DependencyProperty.Register("DeviceRootStatusText", typeof(string), typeof(DeviceInfoPanel),
                new UIPropertyMetadata(defaultStr));
        public string DeviceRootStatusText
        {
            get { return (string)GetValue(DeviceRootStatusTextProperty); }
            set { SetValue(DeviceRootStatusTextProperty, value); }
        }

        public static readonly DependencyProperty DeviceScreenInfoProperty
    = DependencyProperty.Register("DeviceScreenInfo", typeof(string), typeof(DeviceInfoPanel),
        new UIPropertyMetadata(defaultStr));
        public string DeviceScreenInfo
        {
            get { return (string)GetValue(DeviceScreenInfoProperty); }
            set { SetValue(DeviceScreenInfoProperty, value); }
        }

        public static readonly DependencyProperty DeviceRomSizeInfoProperty
    = DependencyProperty.Register("DeviceRomSizeInfo", typeof(string), typeof(DeviceInfoPanel),
        new UIPropertyMetadata(defaultStr));
        public string DeviceRomSizeInfo
        {
            get { return (string)GetValue(DeviceRomSizeInfoProperty); }
            set { SetValue(DeviceRomSizeInfoProperty, value); }
        }

        public static readonly DependencyProperty DeviceRamSizeInfoProperty
    = DependencyProperty.Register("DeviceRamSizeInfo", typeof(string), typeof(DeviceInfoPanel),
        new UIPropertyMetadata(defaultStr));
        public string DeviceRamSizeInfo
        {
            get { return (string)GetValue(DeviceRamSizeInfoProperty); }
            set { SetValue(DeviceRamSizeInfoProperty, value); }
        }

        public static readonly DependencyProperty DeviceAndroidVersionProperty
    = DependencyProperty.Register("DeviceAndroidVersion", typeof(string), typeof(DeviceInfoPanel),
        new UIPropertyMetadata(defaultStr));
        public string DeviceAndroidVersion
        {
            get { return (string)GetValue(DeviceAndroidVersionProperty); }
            set { SetValue(DeviceAndroidVersionProperty, value); }
        }

        public static readonly DependencyProperty DeviceCodeProperty
               = DependencyProperty.Register("DeviceCode", typeof(string), typeof(DeviceInfoPanel),
        new UIPropertyMetadata(defaultStr));
        public string DeviceCode
        {
            get { return (string)GetValue(DeviceCodeProperty); }
            set { SetValue(DeviceCodeProperty, value); }
        }


        public static readonly DependencyProperty DeviceNameProperty
            = DependencyProperty.Register("DeviceName", typeof(string), typeof(DeviceInfoPanel));
        public string DeviceName
        {
            get { return (string)GetValue(DeviceNameProperty); }
            set { SetValue(DeviceNameProperty, value); }
        }

        public static readonly DependencyProperty ConnectStatusTextProperty
            = DependencyProperty.Register("ConnectStatusText", typeof(string), typeof(DeviceInfoPanel));
        public string ConnectStatusText
        {
            get { return (string)GetValue(ConnectStatusTextProperty); }
            set { SetValue(ConnectStatusTextProperty, value); }
        }

        public static readonly DependencyProperty DeviceStateTextProperty
            = DependencyProperty.Register("DeviceStateText", typeof(string), typeof(DeviceInfoPanel));
        public string DeviceStateText
        {
            get { return (string)GetValue(DeviceStateTextProperty); }
            set { SetValue(DeviceStateTextProperty, value); }
        }


        public DeviceBasicInfo DeviceInfo { get; private set; }

        public bool CurrentDeviceIsRoot { get; private set; }

        public DeviceInfoPanel()
        {
            InitializeComponent();
            LanguageHelper.LanguageChanged += (s, e) =>
            {
                Refresh(DeviceInfo);
            };
        }

        public void Refresh(DeviceBasicInfo devSimpleInfo)
        {
            Reset();
            DeviceInfo = devSimpleInfo;
            if (DeviceState.None != DeviceInfo.State)
            {
                ConnectStatusText = App.Current.Resources["lbConnectedDevice"].ToString();
            }
            DeviceStateText = App.Current.Resources[$"deviceState{devSimpleInfo.State}"].ToString();
            switch (devSimpleInfo.State)
            {
                case DeviceState.Poweron:
                case DeviceState.Recovery:
                    PoweronRefresh(devSimpleInfo);
                    break;

                case DeviceState.Fastboot:
                    Task.Run(() =>
                    {
                        var product = new DeviceInfoGetterInFastboot(devSimpleInfo.Serial).GetProduct();
                        Dispatcher.Invoke(() =>
                        {
                            DeviceName = product;
                        });
                    });
                    break;
                case DeviceState.Unauthorized:
                case DeviceState.Offline:
                    break;
            }
        }

        public void Reset()
        {
            DeviceInfo = new DeviceBasicInfo() { State = DeviceState.None };
            Dispatcher.Invoke(() =>
            {
                CurrentDeviceIsRoot = false;
                DeviceStateText = App.Current.Resources[$"deviceStateNone"].ToString();
                ConnectStatusText = App.Current.Resources["lbDisconnect"].ToString();
                ClearValue(DeviceNameProperty);
                ClearValue(DeviceCodeProperty);
                ClearValue(DeviceRootStatusTextProperty);
                ClearValue(DeviceAndroidVersionProperty);
                ClearValue(DeviceScreenInfoProperty);
                ClearValue(DeviceRomSizeInfoProperty);
                ClearValue(DeviceRamSizeInfoProperty);
            });
        }

        private async void PoweronRefresh(DeviceBasicInfo devSimpleInfo)
        {
            var buildProp = await Task.Run(() => { return new DeviceBuildPropGetter(devSimpleInfo.Serial).GetFull(); });
            DeviceName = buildProp.SafeGet(BuildPropKeys.Brand) + " " + buildProp.SafeGet(BuildPropKeys.Model);

            CurrentDeviceIsRoot = await Task.Run(() => { return new DeviceSoftwareInfoGetter(devSimpleInfo.Serial).IsRootEnable(); });
            DeviceRootStatusText = CurrentDeviceIsRoot ? "√" : "×";
            DeviceAndroidVersion = buildProp.SafeGet(BuildPropKeys.AndroidVersion)?.ToString() ?? App.Current.Resources["GetFail"].ToString();
            DeviceCode = buildProp.SafeGet(BuildPropKeys.ProductName) ?? App.Current.Resources["GetFail"].ToString();
            var hwInfo = await Task.Run(() =>
            {
                return new DeviceHardwareInfoGetter(devSimpleInfo.Serial).Get();
            });
            DeviceRomSizeInfo = hwInfo.SizeofRom?.AsGbString() ?? App.Current.Resources["GetFail"].ToString();
            DeviceRamSizeInfo = hwInfo.SizeofRam?.AsGbString() ?? App.Current.Resources["GetFail"].ToString();
            DeviceScreenInfo = hwInfo.ScreenInfo ?? App.Current.Resources["GetFail"].ToString();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Refresh(DeviceInfo);
        }
    }
}
