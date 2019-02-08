/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:40:51 (UTC +8:00)
** desc： ...
*************************************************/

using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 标准的秋之盒拓展基类
    /// </summary>
    [Obsolete]
    public abstract partial class AutumnBoxExtension : ClassExtensionBase
    {
        /// <summary>
        /// 拓展模块参数
        /// </summary>
        protected ExtensionArgs Args { get; private set; }

        /// <summary>
        /// 当拓展被创建后调用
        /// </summary>
        /// <param name="args"></param>
        protected override void OnCreate(ExtensionArgs args)
        {
            base.OnCreate(args);
            Args = args;
            DeviceSelectedOnCreating = DeviceNow;
        }

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
