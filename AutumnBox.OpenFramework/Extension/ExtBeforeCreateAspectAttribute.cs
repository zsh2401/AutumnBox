/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 4:35:43 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 在拓展模块实例创建前的切面类Before()函数的参数
    /// </summary>
    public class ExtBeforeCreateArgs
    {
        /// <summary>
        /// 拓展模块的Type
        /// </summary>
        public Type ExtType { get; set; }
        /// <summary>
        /// 拓展模块创建后的目标设备
        /// </summary>
        public IDevice TargetDevice { get; set; }
        /// <summary>
        /// 截断,这将导致实例不再被创建
        /// </summary>
        public bool Prevent { get; set; } = true;
        /// <summary>
        /// 上下文,通常是Wrapper
        /// </summary>
        public Context Context { get; internal set; }
    }
    /// <summary>
    /// 在拓展模块实例创建前的切面类,包含具体的处理函数Before()
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public abstract class ExtBeforeCreateAspectAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtBeforeCreateAspectAttribute(object value) : base(value) { }
        /// <summary>
        /// 在创建前调用的方法,必须要实现
        /// </summary>
        /// <param name="args"></param>
        public abstract void Before(ExtBeforeCreateArgs args);
    }
}
