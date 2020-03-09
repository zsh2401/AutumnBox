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

using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 通知管理器
    /// </summary>
    public interface INotificationManager
    {
        /// <summary>
        /// 发送一个确认
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="btnConfirmText"></param>
        /// <returns></returns>
        Task SendConfirm(string msg, string btnConfirmText = null);
        /// <summary>
        /// 发送一个是与否的通知
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="btnYesText"></param>
        /// <param name="btnNoText"></param>
        /// <returns></returns>
        Task<bool> SendYN(string msg, string btnYesText = null, string btnNoText = null);
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        void SendMessage(string msg, string title = null);
        /// <summary>
        /// 发送警告信息
        /// </summary>
        /// <param name="warnMsg"></param>
        /// <param name="title"></param>
        void SendWarning(string warnMsg, string title = null);
        /// <summary>
        /// 发送错误信息
        /// </summary>
        /// <param name="errorMsg"></param>
        /// <param name="title"></param>
        void SendError(string errorMsg, string title = null);
    }
}
