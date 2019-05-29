using System;
using System.Collections.Generic;
using System.Text;
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Management;

namespace AutumnBox.OpenFramework.Open.Impl
{
    class DeviceSelectorImpl : IDeviceSelector
    {
        public IDevice GetCurrent()
        {
            return OpenFx.BaseApi.SelectedDevice;
        }
    }
}
