using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Service;
using System;

namespace AutumnBox.OpenFramework.Open.ServiceImpl
{
    [ServiceName(ServicesNames.DEVICE_SELECTOR)]
    class SDeviceSelector : AtmbService, IDeviceSelector
    {
        public IDevice GetCurrent(Context ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }

            return CallingBus.BaseApi.SelectedDevice;
        }
    }
}
