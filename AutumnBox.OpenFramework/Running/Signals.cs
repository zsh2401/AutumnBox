using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Running
{
    /// <summary>
    /// 信号
    /// </summary>
    public static class Signals
    {
        /// <summary>
        /// 当模块进程被请求杀死时发生
        /// </summary>
        public const string COMMAND_STOP = "COMMAND_STOP";
        /// <summary>
        /// 摧毁参数
        /// </summary>
        public const string COMMAND_DESTORY = "COMMAND_DESTORY";
        /// <summary>
        /// 已创建
        /// </summary>
        public const string ON_CREATED = "ON_CREATED";
    }
}
