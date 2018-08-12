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
    public class ExtBeforeCreateArgs
    {
        public Type ExtType { get; set; }
        public DeviceBasicInfo TargetDevice { get; set; }
        public bool Prevent { get; set; } = true;
        public Context Context { get; internal set; }
    }
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class ExtBeforeCreateAspectAttribute : ExtInfoAttribute
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public ExtBeforeCreateAspectAttribute(object value) : base(value) { }
        public abstract void Before(ExtBeforeCreateArgs args);
    }
}
