/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 15:59:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    partial class AutumnBoxExtension
    {
        /// <summary>
        /// 日志标签
        /// </summary>
        public override string LoggingTag => Args.Wrapper.Info.Name;
        /// <summary>
        /// 目标设备
        /// </summary>
        public IDevice TargetDevice => Args.TargetDevice;
        /// <summary>
        /// Wrapper
        /// </summary>
        public IExtensionWrapper Wrapper => Args.Wrapper;
    }
}
