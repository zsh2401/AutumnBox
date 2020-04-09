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
        /// 发送一个通知
        /// </summary>
        /// <param name="msg">内容</param>
        /// <param name="title">标题</param>
        /// <param name="onClickHandler">当点击时的执行函数</param>
        void SendNotification(string msg, string title = null, Action onClickHandler = null);
    }
}
