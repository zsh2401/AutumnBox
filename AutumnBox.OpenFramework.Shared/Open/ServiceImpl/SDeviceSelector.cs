using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Service;
using AutumnBox.OpenFramework.Service.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open.ServiceImpl
{
    [ServiceName(ServicesNames.DEVICE_SELECTOR)]
    class SDeviceSelector : AtmbService, IDeviceSelector
    {
        private readonly IBaseApi baseApi;
        public SDeviceSelector()
        {
            var container = GetService<SBaseApiContainer>(SBaseApiContainer.NAME);
            baseApi = container.GetApi(this);
        }
        public IDevice GetCurrent(Context ctx)
        {
            if (ctx == null)
            {
                throw new ArgumentNullException(nameof(ctx));
            }

            return baseApi.SelectedDevice;
        }
    }
}
