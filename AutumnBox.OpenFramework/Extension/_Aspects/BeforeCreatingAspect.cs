/*************************************************
** auth： zsh2401@163.com
** date:  2018/10/18 20:00:56 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 创建前切面的参数
    /// </summary>
    public class BeforeCreatingAspectArgs
    {
        /// <summary>
        /// 上下文
        /// </summary>
        public Context Context { get; set; }
        /// <summary>
        /// 拓展模块的Type
        /// </summary>
        public Type ExtensionType { get; set; }
        /// <summary>
        /// 目标设备
        /// </summary>
        public IDevice TargetDevice { get; set; }
    }
    /// <summary>
    /// 创建前切面的基础实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class BeforeCreatingAspect : Attribute,IBeforeCreatingAspect
    {
        /// <summary>
        /// 具体实现
        /// </summary>
        /// <param name="args"></param>
        /// <param name="canContinue"></param>
        public abstract void BeforeCreating(BeforeCreatingAspectArgs args, ref bool canContinue);
    }
}
