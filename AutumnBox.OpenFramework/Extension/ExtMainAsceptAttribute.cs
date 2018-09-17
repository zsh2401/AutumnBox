/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 4:19:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行前函数的参数
    /// </summary>
    public class BeforeArgs
    {
        /// <summary>
        /// 截断
        /// 默认为false
        /// </summary>
        public bool Prevent { get; set; } = false;
        /// <summary>
        /// 目标设备
        /// </summary>
        public IDevice TargetDevice { get; set; }
        /// <summary>
        /// 拓展模块
        /// </summary>
        public AutumnBoxExtension Extension { get; private set; }
        /// <summary>
        /// 拓展包装器
        /// </summary>
        public IExtensionWrapper ExtWrapper { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="extension"></param>
        public BeforeArgs(AutumnBoxExtension extension)
        {
            this.Extension = extension;
        }
    }
    /// <summary>
    /// 运行后函数的参数
    /// </summary>
    public class AfterArgs
    {
        /// <summary>
        /// 是否是被强制停止的
        /// </summary>
        public bool IsForceStopped { get; set; } = false;
        /// <summary>
        /// 返回码
        /// </summary>
        public int ReturnCode { get; set; }
        /// <summary>
        /// 拓展模块
        /// </summary>
        public AutumnBoxExtension Extension { get; set; }
        /// <summary>
        /// 拓展包装器
        /// </summary>
        public IExtensionWrapper ExtWrapper { get; set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="extension"></param>
        public AfterArgs(AutumnBoxExtension extension)
        {
            this.Extension = extension;
        }
    }
    /// <summary>
    /// 拓展模块主函数切面
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExtMainAsceptAttribute : Attribute
    {
        /// <summary>
        /// 在Main前运行
        /// </summary>
        /// <param name="args"></param>
        public virtual void Before(BeforeArgs args) { }
        /// <summary>
        /// 在Main后运行
        /// </summary>
        /// <param name="args"></param>
        public virtual void After(AfterArgs args) { }
    }
}
