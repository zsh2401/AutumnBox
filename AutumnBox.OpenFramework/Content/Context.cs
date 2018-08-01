/*************************************************
** auth： zsh2401@163.com
** date:  2018/3/6 16:48:15 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using System;

namespace AutumnBox.OpenFramework.Content
{
    /// <summary>
    /// AutumnBox开放框架上下文
    /// </summary>
    public abstract class Context : Object
    {
        private ContextApiProvider apiWarpper;
        /// <summary>
        /// 日志标签
        /// </summary>
        public abstract string LoggingTag { get; }
        /// <summary>
        /// 日志API
        /// </summary>
        protected ILogger Logger => apiWarpper.Logger;
        /// <summary>
        /// 秋之盒整体程序相关API
        /// </summary>
        protected IAppManager GlobalManager => apiWarpper.App;
        /// <summary>
        /// 操作系统API
        /// </summary>
        protected IOSApi OperatingSystem => apiWarpper.OS;
        /// <summary>
        /// 临时文件管理器
        /// </summary>
        protected ITemporaryFloder Tmp => apiWarpper.Tmp;
        /// <summary>
        /// 兼容性相关API
        /// </summary>
        protected ICompApi Comp => apiWarpper.Comp;
        /// <summary>
        /// 构建
        /// </summary>
        public Context()
        {
            apiWarpper = new ContextApiProvider(this);
        }
    }
}