/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:03:31 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Warpper;
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
        /// 显示消息窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        void ShowMessageDialog(string title, string message);
        /// <summary>
        /// 显示消息窗口,使用默认标题
        /// </summary>
        /// <param name="message"></param>
        void ShowMessageDialog(string message);
        /// <summary>
        /// 显示警告窗口
        /// </summary>
        /// <param name="message"></param>
        void ShowWarnDialog(string message);
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
