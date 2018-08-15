/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 17:52:01 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutumnBox.Basic.Device;
using AutumnBox.Support.Log;

namespace AutumnBox.GUI.UI.Model.Panel
{
    sealed class ModelDeviceDetails : ModelBase, IDependOnDeviceChanges
    {
        private const string DEFAULT_VALUE = "...";

        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
                RaisePropertyChanged("Brand");
            }
        }

        public INotifyDeviceChanged NotifyDeviceChanged
        {
            set
            {
                value.DeviceChanged += DevicesChanged;
                value.NoDevice += Reset;
            }
        }

        private string _brand = DEFAULT_VALUE;

        public void Reset(object sender, EventArgs args)
        {
            Brand = DEFAULT_VALUE;
        }

        public void DevicesChanged(object sender, DeviceChangedEventArgs args)
        {
            Brand = "test";
        }
    }
}
