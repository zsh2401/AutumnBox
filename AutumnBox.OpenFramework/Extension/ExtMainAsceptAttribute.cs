/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/13 4:19:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Content;
using System;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 运行前函数的参数
    /// </summary>
    public class BeforeArgs
    {
        public Context Context { get; internal set; }
        public bool Prevent { get; set; } = false;
        public DeviceBasicInfo TargetDevice { get; set; }
    }
    /// <summary>
    /// 运行后函数的参数
    /// </summary>
    public class AfterArgs {
        public Context Context { get; internal set; }
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
