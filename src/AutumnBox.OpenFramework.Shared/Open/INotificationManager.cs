/*

* ==============================================================================
*
* Filename: INotificationManager
* Description: 
*
* Version: 1.0
* Created: 2020/3/10 1:48:41
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/

using System;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 通知管理器
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// 线程安全地发送消息
        /// </summary>
        /// <param name="msg"></param>
        void Info(string msg);

        /// <summary>
        /// 线程安全地向用户询问
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        Task<bool> Ask(string msg);

        /// <summary>
        /// 线程安全地发送警告
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);

        /// <summary>
        /// 线程安全地发送成功消息
        /// </summary>
        /// <param name="msg"></param>
        void Success(string msg);
    }
}
