/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 22:48:07 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Warpper;
using AutumnBox.Support.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutumnBox.GUI.UI.Model.Panel
{
    class ModelExtensions : ModelBase, IDependOnDeviceChanges
    {
        public class WarpperWarpper
        {
            public IExtensionWarpper Warpper { get; private set; }
            public string Name => Warpper.Info.Name;
            public ImageSource Icon
            {
                get
                {
                    if (icon == null) LoadIcon();
                    return icon;
                }
            }
            private ImageSource icon;
            private WarpperWarpper(IExtensionWarpper warpper)
            {
                this.Warpper = warpper;
            }
            private void LoadIcon()
            {
                if (Warpper.Info.Icon == null)
                {
                    icon = App.Current.Resources["DefaultExtensionIcon"] as ImageSource;
                }
                else
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.StreamSource = new MemoryStream(Warpper.Info.Icon);
                    bmp.EndInit();
                    bmp.Freeze();
                    icon = bmp;
                }
            }
            public static IEnumerable<WarpperWarpper> From(IEnumerable<IExtensionWarpper> warppers)
            {
                List<WarpperWarpper> result = new List<WarpperWarpper>();
                foreach (var warpper in warppers)
                {
                    result.Add(new WarpperWarpper(warpper));
                }
                return result;
            }
        }
        #region MVVM
        public List<WarpperWarpper> Warppers
        {
            get
            {
                return _warppers;
            }
            set
            {
                _warppers = value;
                RaisePropertyChanged(nameof(Warppers));
            }
        }
        private List<WarpperWarpper> _warppers;

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged(nameof(SelectedIndex));
                SetDataByIndex(_selectedIndex);
            }
        }
        private int _selectedIndex = -1;

        public Visibility DetailsVisibily
        {
            get
            {
                return visibility;
            }
            set
            {
                visibility = value;
                RaisePropertyChanged(nameof(DetailsVisibily));
            }
        }
        private Visibility visibility = Visibility.Collapsed;

        public string BtnRunText
        {
            get
            {
                return _btnRunText;
            }
            set
            {
                _btnRunText = value;
                RaisePropertyChanged(nameof(BtnRunText));
            }
        }
        private string _btnRunText;

        public IExtensionWarpper Selected
        {
            get
            {
                return _selectedWarpper;
            }
            set
            {
                _selectedWarpper = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }
        private IExtensionWarpper _selectedWarpper;

        public bool BtnRunEnabled
        {
            get
            {
                return _btnRunEnabled;
            }
            set
            {
                BtnRunText = value ?
                    App.Current.Resources["btnRunExtension"].ToString()
                    : App.Current.Resources["btnCannotRunExtension"].ToString();

                _btnRunEnabled = value;
                RaisePropertyChanged(nameof(BtnRunEnabled));
            }
        }
        private bool _btnRunEnabled = false;
        #endregion

        #region InjectAndEvents
        public INotifyDeviceChanged NotifyDeviceChanged
        {
            set
            {
                value.DeviceChanged += Value_DeviceChanged;
                value.NoDevice += Value_NoDevice;
            }
        }

        private void Value_NoDevice(object sender, EventArgs e)
        {
            SelectedIndex = -1;
            DetailsVisibily = Visibility.Collapsed;
            CurrentDevcie = DeviceBasicInfo.None;
            BtnRunEnabled = false;
        }

        private void Value_DeviceChanged(object sender, DeviceChangedEventArgs e)
        {
            bool hasState = (e.CurrentDevice.State & targetState) != 0;
            BtnRunEnabled = hasState;
            CurrentDevcie = e.CurrentDevice;
        }
        #endregion

        public DeviceBasicInfo CurrentDevcie { get; set; }

        public void SetDataByIndex(int index)
        {
            if (index == -1)
            {
                DetailsVisibily = Visibility.Collapsed;
            }
            else
            {
                DetailsVisibily = Visibility.Visible;
                var item = Warppers[index].Warpper;
                Selected = item;
            }
        }

        private readonly DeviceState targetState;
        public ModelExtensions(DeviceState targetState)
        {
            this.targetState = targetState;
            var filted = from warpper in OpenFramework.Management.Manager.InternalManager.Warppers
                         where (warpper.Info.RequiredDeviceStates & targetState) != 0
                         select warpper;
            BtnRunText = App.Current.Resources["btnCannotRunExtension"].ToString();
            Warppers = WarpperWarpper.From(filted).ToList();
        }
    }
}
