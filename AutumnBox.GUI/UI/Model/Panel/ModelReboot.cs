/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 18:42:25 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Support.Log;
using System;

namespace AutumnBox.GUI.UI.Model.Panel
{
    class ModelReboot : ModelBase, IDependOnDeviceChanges
    {
        public bool BtnSystemEnabled
        {
            get
            {
                return _btnSystemEnabled;
            }
            set
            {
                _btnSystemEnabled = value;
                RaisePropertyChanged(nameof(BtnSystemEnabled));
            }
        }
        private bool _btnSystemEnabled = false;

        public bool BtnFastbootEnabled
        {
            get
            {
                return _btnFastbootEnabled;
            }
            set
            {
                _btnFastbootEnabled = value;
                RaisePropertyChanged(nameof(BtnFastbootEnabled));
            }
        }
        private bool _btnFastbootEnabled = false;

        public bool BtnRecoveryEnabled
        {
            get
            {
                return _btnRecoveryEnabled;
            }
            set
            {
                _btnRecoveryEnabled = value;
                RaisePropertyChanged(nameof(BtnRecoveryEnabled));
            }
        }
        private bool _btnRecoveryEnabled = false;

        public bool Btn9008Enabled
        {
            get
            {
                return _btn9008Enabled;
            }
            set
            {
                _btn9008Enabled = value;
                RaisePropertyChanged(nameof(Btn9008Enabled));
            }
        }
        private bool _btn9008Enabled = false;

        public DeviceBasicInfo CurrentDevice { get; set; }
        public INotifyDeviceChanged NotifyDeviceChanged
        {
            set
            {
                value.DeviceChanged += DeviceChanged;
                value.NoDevice += NoDevice;
            }
        }

        public void DeviceChanged(object sender, DeviceChangedEventArgs args)
        {
            CurrentDevice = args.CurrentDevice;
            BtnSystemEnabled = true;
            Btn9008Enabled = true;
            BtnFastbootEnabled = true;
            BtnRecoveryEnabled = (CurrentDevice.State != DeviceState.Fastboot);
        }
        public void NoDevice(object sender, EventArgs args)
        {
            CurrentDevice = new DeviceBasicInfo()
            {
                State = DeviceState.None
            };
            BtnRecoveryEnabled = false;
            BtnSystemEnabled = false;
            Btn9008Enabled = false;
            BtnFastbootEnabled = false;
        }
    }
}
