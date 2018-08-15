/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/15 16:14:56 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.GUI.UI.Model.Panel
{
    class ModelPanelDevices : ModelBase
    {
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
            }
        }
        private int _selectedIndex;
        private DevicesMonitor.DevicesMonitorCore core = new DevicesMonitor.DevicesMonitorCore();
        public List<DeviceBasicInfo> Devices
        {
            get
            {
                return _devices;
            }
            set
            {
                _devices = value;
                RaisePropertyChanged(nameof(Devices));
            }
        }
        private List<DeviceBasicInfo> _devices = new List<DeviceBasicInfo>();
        public bool BtnConnectEnable
        {
            get
            {
                return btnConnectEnable;
            }
            set
            {
                btnConnectEnable = value;
                RaisePropertyChanged("BtnConnectEnable");
            }
        }
        private bool btnConnectEnable = false;
        public void StartListening()
        {
            core.DevicesChanged += (s, e) =>
            {
                Devices = e.DevicesList.ToList();
                if (Devices.Count > 0)
                {
                    SelectedIndex = 0;
                }
            };
            core.Begin();
        }
    }
}
