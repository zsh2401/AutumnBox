using AutumnBox.Basic.Device;
using System;

namespace AutumnBox.OpenFramework.Exceptions
{
    /// <summary>
    /// 目标设备状态不正确
    /// </summary>
    public class DeviceStateIsNotCorrectException : Exception
    {
        /// <summary>
        /// 需要的状态
        /// </summary>
        public DeviceState RequiredState { get; }

        /// <summary>
        /// 目标设备状态
        /// </summary>
        public DeviceState? CurrentDeviceState { get; }

        /// <summary>
        /// 目标设备不正确
        /// </summary>
        public DeviceStateIsNotCorrectException(DeviceState reqState, DeviceState? crtDevState)
            : base($"Target device state is not correct.Required:{reqState},current:{crtDevState?.ToString() ?? "device not connect"}")
        {
            this.RequiredState = reqState;
            this.CurrentDeviceState = crtDevState;
        }
    }
}
