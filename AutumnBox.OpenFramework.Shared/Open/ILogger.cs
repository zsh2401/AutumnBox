/*

* ==============================================================================
*
* Filename: ILogger
* Description: 
*
* Version: 1.0
* Created: 2020/3/3 16:18:37
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 日志器
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 打印一条日志,此日志仅在开启秋之盒DEBUG模式时可用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="content"></param>
        void Debug(object sender, object content);
        /// <summary>
        /// 打印一条INFO级别日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="content"></param>
        void Info(object sender, object content);
        /// <summary>
        /// 打印一条警告级别日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="content"></param>
        void Warn(object sender, object content);
        /// <summary>
        /// 打印一条严重错误级别日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="content"></param>
        void Fatal(object sender, object content);
    }
}
