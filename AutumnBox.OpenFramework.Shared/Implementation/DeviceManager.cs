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
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Open.ADBKit;
using System;

namespace AutumnBox.OpenFramework.Implementation
{
    class DeviceManager : IDeviceManager
    {

        private readonly IBaseApi baseApi;

        public DeviceManager(IBaseApi baseApi)
        {
            if (baseApi is null)
            {
                throw new ArgumentNullException(nameof(baseApi));
            }

            this.baseApi = baseApi;
        }
        public IDevice Selected { get => baseApi.SelectedDevice; set => throw new NotImplementedException(); }

        public IDevice[] ConnectedDevices => throw new NotImplementedException();
    }
}
