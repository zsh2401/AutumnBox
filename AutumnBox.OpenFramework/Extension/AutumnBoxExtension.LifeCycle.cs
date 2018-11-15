/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/1 0:40:51 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Content;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Wrapper;
using System;

namespace AutumnBox.OpenFramework.Extension
{

    /// <summary>
    /// 标准的秋之盒拓展基类
    /// </summary>
    public abstract partial class AutumnBoxExtension : ClassExtensionBase, IClassExtension
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
    }
}
