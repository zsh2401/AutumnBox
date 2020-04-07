using System;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.MultipleDevices;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.ADBKit;

namespace AutumnBox.OpenFramework.Implementation
{
    class DeviceSelectorImpl : IDeviceManager
    {
        private readonly IBaseApi baseApi;
        public DeviceSelectorImpl(IBaseApi baseApi)
        {
            if (baseApi is null)
            {
                throw new System.ArgumentNullException(nameof(baseApi));
            }

            this.baseApi = baseApi;
        }

        public IDevice Selected { get => baseApi.SelectedDevice; set => throw new System.NotImplementedException(); }

        public IDevice[] ConnectedDevices => throw new System.NotImplementedException();

        public event DevicesChangedHandler DevicesChanged;
        public event EventHandler SelectedDeviceChanged;
    }
}
