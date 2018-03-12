using AutumnBox.Basic.Device;
using AutumnBox.GUI.Helper;
using AutumnBox.GUI.I18N;
using AutumnBox.GUI.Resources;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.UI.FuncPanels
{
    internal static class DictExt
    {
        public static string Get(this Dictionary<string, string> dict, string key)
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
    public partial class DeviceInfoPanel : UserControl, IAsyncRefreshable
    {
        public DeviceBasicInfo DeviceInfo { get; private set; }
        public Version CurrentDeviceAndroidVersion { get; private set; }
        public bool CurrentDeviceIsRoot { get; private set; }
        public DeviceInfoPanel()
        {
            InitializeComponent();
        }
        public event EventHandler RefreshStart;
        public event EventHandler RefreshFinished;
        private void SetStatusShow(ImageSource source, string key)
        {
            this.DeviceStatusImage.Source = source;
            this.DeviceStatusLabel.Content = App.Current.Resources[key] ?? key;
        }
        public void Refresh(DeviceBasicInfo devSimpleInfo)
        {
            Reset();
            this.DeviceInfo = devSimpleInfo;
            //return;
            switch (devSimpleInfo.State)
            {
                case DeviceState.Poweron:
                case DeviceState.Recovery:
                    SetByDeviceSimpleInfoAsync(devSimpleInfo);
                    RefreshStart?.Invoke(this, new EventArgs());
                    break;
                case DeviceState.Unauthorized:
                    UIHelper.SetGridLabelsContent(GridBuildInfo, App.Current.Resources["PleaseAllowUSBDebug"]);
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "PleaseAllowUSBDebug");
                    break;
                case DeviceState.Fastboot:
                    UIHelper.SetGridLabelsContent(GridBuildInfo, "...");
                    UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                    UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "DeviceInFastboot");
                    break;
                case DeviceState.Offline:
                    UIHelper.SetGridLabelsContent(GridBuildInfo, "...");
                    UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                    UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "DeviceOffline");
                    break;
            }
        }
        public void Reset()
        {
            Logger.Info(this,"Reseting");
            this.Dispatcher.Invoke(() =>
            {
                UIHelper.SetGridLabelsContent(GridBuildInfo, App.Current.Resources["PleaseSelectedADevice"]);
                UIHelper.SetGridLabelsContent(GridHardwareInfo, "....");
                UIHelper.SetGridLabelsContent(GridMemoryInfo, "....");
                SetStatusShow(DevStatusBitmapGetter.Get(DeviceState.None), "PleaseSelectedADevice");
            });
        }
        private async void SetByDeviceSimpleInfoAsync(DeviceBasicInfo devSimpleInfo)
        {
            var propGetter = new DeviceBuildPropGetter(devSimpleInfo.Serial);
            Dictionary<string, string> buildInfo = await Task.Run(() =>
                 {
                     return propGetter.GetFull();
                 });

            LabelAndroidVersion.Content = buildInfo.Get(BuildPropKeys.AndroidVersion) ?? App.Current.Resources["GetFail"].ToString();
            string brandAndModel = null;
            string brand = buildInfo.Get(BuildPropKeys.Brand);
            string model = buildInfo.Get(BuildPropKeys.Model);
            if (brand != null && model != null)
            {
                brandAndModel = brand + " " + model;
            }
            LabelModel.Content = brandAndModel ?? App.Current.Resources["GetFail"].ToString();
            LabelCode.Content = buildInfo.Get(BuildPropKeys.ProductName) ?? App.Current.Resources["GetFail"].ToString();

            LabelRom.Content = App.Current.Resources["Getting"].ToString();
            LabelRam.Content = App.Current.Resources["Getting"].ToString();
            LabelBattery.Content = App.Current.Resources["Getting"].ToString();
            LabelSOC.Content = App.Current.Resources["Getting"].ToString();
            LabelScreen.Content = App.Current.Resources["Getting"].ToString();
            LabelFlashMemInfo.Content = App.Current.Resources["Getting"].ToString();
            LabelRootStatus.Content = App.Current.Resources["Getting"].ToString();
            try
            {
                CurrentDeviceAndroidVersion = new Version(LabelAndroidVersion.Content.ToString());
            }
            catch
            {
                CurrentDeviceAndroidVersion = null;
            }
            switch (devSimpleInfo.State)
            {
                case DeviceState.Fastboot:
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "DeviceInFastboot");
                    break;
                case DeviceState.Recovery:
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "DeviceInRecovery");
                    break;
                case DeviceState.Poweron:
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "DeviceInRunning");
                    break;
                case DeviceState.Sideload:
                    SetStatusShow(DevStatusBitmapGetter.Get(devSimpleInfo.State), "DeviceInSideload");
                    break;
            }
            RefreshFinished?.Invoke(this, new EventArgs());
            
            var advInfo = await Task.Run(() =>
            {
                return new DeviceHardwareInfoGetter(DeviceInfo.Serial).Get();
            });
            LabelRom.Content = (advInfo.SizeofRom != null) ? advInfo.SizeofRom + "GB" : App.Current.Resources["GetFail"].ToString();
            LabelRam.Content = (advInfo.SizeofRam != null) ? advInfo.SizeofRam + "GB" : App.Current.Resources["GetFail"].ToString();
            LabelBattery.Content = (advInfo.BatteryLevel != null) ? advInfo.BatteryLevel + "%" : App.Current.Resources["GetFail"].ToString();
            LabelSOC.Content = advInfo.SOCInfo ?? App.Current.Resources["GetFail"].ToString();
            LabelScreen.Content = advInfo.ScreenInfo ?? App.Current.Resources["GetFail"].ToString();
            LabelFlashMemInfo.Content = advInfo.FlashMemoryType ?? App.Current.Resources["GetFail"].ToString();
            bool IsRoot = await Task.Run(() => { return new DeviceSoftwareInfoGetter(devSimpleInfo.Serial).IsRootEnable(); });
            LabelRootStatus.Content = IsRoot ? App.Current.Resources["RootEnable"].ToString() : App.Current.Resources["RootDisable"].ToString();
            CurrentDeviceIsRoot = IsRoot;
        }

        private void LabelAndroidVersion_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            LanguageHelper.LanguageChanged += (s, ex) => { Refresh(DeviceInfo); };
        }

        private void ImgRefreshDevInfo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Refresh(DeviceInfo);
        }
    }
}
