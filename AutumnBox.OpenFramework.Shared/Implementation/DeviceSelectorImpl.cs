using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open;

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
        public IDevice GetCurrent()
        {
            return baseApi.SelectedDevice;
        }
    }
}
