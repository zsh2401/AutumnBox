/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:03:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 秋之盒用户交互API
    /// </summary>
    public interface IUx
    {
        /// <summary>
        /// 目前选择的设备
        /// </summary>
        IDevice CurrentSelectedDevice { get; }
        /// <summary>
        /// 询问用户是否同意字符串内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        bool Agree(string message);
        /// <summary>
        /// 进行是否的抉择
        /// </summary>
        /// <param name="message"></param>
        /// <param name="btnOK"></param>
        /// <param name="btnNO"></param>
        /// <returns></returns>
        bool DoYN(string message, string btnOK = null, string btnNO = null);

        /// <summary>
        /// 让用户做选择
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="btnLeft"></param>
        /// <param name="btnRight"></param>
        /// <param name="btnCancel"></param>
        /// <returns></returns>
        ChoiceResult DoChoice(string message, string btnLeft = null, string btnRight = null, string btnCancel = null);
        /// <summary>
        /// 信息，通常情况下，此方法会阻塞至用户确认
        /// 使用默认标题
        /// </summary>
        /// <param name="message">信息</param>
        void Message(string message);
       /// <summary>
       /// 显示信息，通常情况下，此方法会阻塞至用户确认
       /// </summary>
       /// <param name="title">标题</param>
       /// <param name="message">信息</param>
        void Message(string title, string message);
        /// <summary>
        /// 警告，通常情况下，此方法会阻塞至用户确认
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);
        /// <summary>
        /// 错误，通常情况下，此方法会阻塞至用户确认
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);
        /// <summary>
        /// 显示调试窗口
        /// </summary>
        void ShowDebuggingWindow();
        /// <summary>
        /// 显示加载窗口
        /// </summary>
        [Obsolete]
        void ShowLoadingWindow();
        /// <summary>
        /// 关闭加载窗口
        /// </summary>
        [Obsolete]
        void CloseLoadingWindow();
        /// <summary>
        /// 在UI线程中运行代码
        /// </summary>
        void RunOnUIThread(Action action);
    }
}
