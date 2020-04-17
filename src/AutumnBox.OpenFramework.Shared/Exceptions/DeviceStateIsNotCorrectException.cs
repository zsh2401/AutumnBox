using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Exceptions
{
    /// <summary>
    /// 目标设备状态不正确
    /// </summary>
    public class DeviceStateIsNotCorrectException : Exception
    {
        /// <summary>
        /// 目标设备不正确
        /// </summary>
        public DeviceStateIsNotCorrectException() : base("Target device state is not correct") { }
    }
}
