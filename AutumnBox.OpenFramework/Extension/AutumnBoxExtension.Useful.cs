/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 15:59:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    partial class AutumnBoxExtension
    {
        /// <summary>
        /// 当前的目标设备
        /// </summary>
        protected IDevice DeviceNow
        {
            get
            {
                IDeviceSelector selector = GetService<IDeviceSelector>(ServicesNames.DEVICE_SELECTOR);
                return selector.GetCurrent(this);
            }
        }
        /// <summary>
        /// 当模块被创建时选择的设备
        /// </summary>
        protected IDevice DeviceSelectedOnCreating { get; private set; }
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => Args.Wrapper.Info.Name;
        /// <summary>
        /// 目标设备
        /// </summary>
        [Obsolete("Plz use DeviceSelectedOnCreating or DeviceNow to instead")]
        public IDevice TargetDevice => DeviceSelectedOnCreating;
        /// <summary>
        /// Wrapper
        /// </summary>
        public IExtensionWrapper Wrapper => Args.Wrapper;
    }
}
