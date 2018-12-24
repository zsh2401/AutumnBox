/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/24 18:10:09 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Exceptions
{
    /// <summary>
    /// 模块无法停止异常
    /// </summary>
    public class ExtensionCantBeStoppedException : Exception
    {
        /// <summary>
        /// 构造异常
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public ExtensionCantBeStoppedException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
        /// <summary>
        /// 构造异常
        /// </summary>
        /// <param name="message"></param>
        public ExtensionCantBeStoppedException(string message)
        : base(message)
        {

        }
    }
}
