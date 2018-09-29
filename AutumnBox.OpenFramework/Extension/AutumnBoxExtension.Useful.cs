/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 15:59:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Exceptions;
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
        /// <summary>
        /// 获取此拓展是否被请求取消
        /// </summary>
        public bool Canceled { get; internal set; } = false;
        /// <summary>
        /// 当被请求取消时抛出异常
        /// </summary>
        protected void ThrowIfCanceled()
        {
            if (Canceled)
            {
                throw new ExtensionCanceledException();
            }
        }
        /// <summary>
        /// 传入一个Action,如果此模块未被取消,才会运行
        /// 此函数很有用,请善用
        /// </summary>
        /// <param name="action"></param>
        protected void Step(Action action)
        {
            if (!Canceled)
            {
                action();
            }
        }
    }
}
