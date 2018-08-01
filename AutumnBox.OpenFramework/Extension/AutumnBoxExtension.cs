/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:40:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Extension.Attributes;
using System;

namespace AutumnBox.OpenFramework.Extension
{

    /// <summary>
    /// 标准的秋之盒拓展基类
    /// </summary>
    [ExtName("标准秋之盒拓展")]
    [ExtAuth("zsh2401")]
    [ExtDesc("这是一个测试模块")]
    [ExtVersion(1,0,0)]
    [ExtRequiredDeviceStates(DeviceState.Poweron)]
    public abstract class AutumnBoxExtension : Context
    {
        /// <summary>
        /// 日志标签
        /// </summary>
        public sealed override string LoggingTag => loggingTag;
        private readonly string loggingTag;
        /// <summary>
        /// 目标设备
        /// </summary>
        protected readonly DeviceBasicInfo TargetDevice;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="args"></param>
        public AutumnBoxExtension(CreatingArgs args)
        {
            loggingTag = args.LoggingTag;
            TargetDevice = args.Deivce;
        }
        /// <summary>
        /// 主进程
        /// </summary>
        public abstract int Main();
        /// <summary>
        /// 当用户要求终止时调用
        /// </summary>
        /// <returns></returns>
        public virtual bool OnStopCommand()
        {
            throw new NotImplementedException();
        }
    }
}
