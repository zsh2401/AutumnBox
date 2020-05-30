/*

* ==============================================================================
*
* Filename: DeviceManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/17 0:21:02
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.Leafx.Container.Support;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.ADBKit;
using System;
using System.Linq;

namespace AutumnBox.OpenFramework.Implementation
{
    [Component(Type = typeof(IDeviceManager))]
    class DeviceManager : IDeviceManager
    {
        readonly IBaseApi baseApi;
        public DeviceManager(IBaseApi baseApi)
        {
            this.baseApi = baseApi;
            baseApi.SelectedDeviceChanged += (s, e) =>
            {
                this.SelectedDeviceChanged?.Invoke(this, new EventArgs());
            };
            baseApi.ConnectedDeviceChanged += (s, e) =>
            {
                this.DevicesChanged?.Invoke(this, new DevicesChangedEventArgs(ConnectedDevices));
            };
        }

        public IDevice Selected { get => baseApi!.SelectedDevice; set => baseApi!.SelectedDevice = value; }

        public IDevice[] ConnectedDevices => baseApi!.ConnectedDevices.ToArray();

        public event DevicesChangedHandler? DevicesChanged;
        public event EventHandler? SelectedDeviceChanged;
    }
}
