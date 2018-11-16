/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/22 23:06:30 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;

namespace AutumnBox.OpenFramework.Extension
{
    /// <summary>
    /// 用于在拓展模块被创建时的参数
    /// </summary>
    public class ExtensionArgs : EventArgs
    {
        /// <summary>
        /// 目前的拓展模块进程
        /// </summary>
        public IExtensionProcess CurrentProcess { get; set; }
        /// <summary>
        /// 包装器
        /// </summary>
        public IExtensionWrapper Wrapper { get; set; }
        /// <summary>
        /// 拓展数据，通常为null
        /// </summary>
        public Dictionary<string,object> ExtractData { get; set; } = null;
    }
    /// <summary>
    /// 当拓展模块即将被摧毁时的参数
    /// </summary>
    public class ExtensionDestoryArgs : EventArgs
    {

    }
    /// <summary>
    /// IClassExtension.OnStopCommand()的参数
    /// </summary>
    public class ExtensionStopArgs : EventArgs {

    }
    /// <summary>
    /// IClassExtension.OnFinished()的参数
    /// </summary>
    public class ExtensionFinishedArgs : EventArgs
    {
        /// <summary>
        /// 返回码
        /// </summary>
        public int ExitCode { get; set; }
        /// <summary>
        /// 是否是被强制停止的
        /// </summary>
        public bool IsForceStopped { get; set; }
    }
}
