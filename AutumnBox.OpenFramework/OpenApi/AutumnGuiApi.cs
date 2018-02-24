/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/23 20:23:20 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.OpenApi
{
    public static class AutumnGuiApi
    {
        private static bool isInited = false;
        private static Func<string, string, string, string, bool?> showChoiceBoxFunc;
        private static Action<string, string> showMessageBox;
        private static Action<ICompletable> showLoadingWindow;
        public static void Init(
            Func<string, string, string, string, bool?> _showChoiceBoxFunc,
            Action<string, string> _showMessageBox,
            Action<ICompletable> _showLoadingWindow)
        {
            if (isInited) return;
            showChoiceBoxFunc = _showChoiceBoxFunc;
            showMessageBox = _showMessageBox;
            showLoadingWindow = _showLoadingWindow;
            isInited = true;
        }

        /// <summary>
        /// 显示一个选项窗口并获取选择值
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="btnLeft"></param>
        /// <param name="btnRight"></param>
        /// <returns></returns>
        public static bool? ShowChoiceBox(string title, string msg,
            string btnLeft = "", string btnRight = "")
        {
            return showChoiceBoxFunc?.Invoke(title, msg, btnLeft, btnRight);
        }
        /// <summary>
        /// 实现一个信息窗
        /// </summary>
        /// <param name="titKeyOrValue"></param>
        /// <param name="msgKeyOrValue"></param>
        public static void ShowMessageBox(string titKeyOrValue, string msgKeyOrValue) {
            showMessageBox?.Invoke(titKeyOrValue, msgKeyOrValue);
        }
        /// <summary>
        /// 调用此方法将会阻塞线程直到ICompletable触发Finished事件
        /// </summary>
        /// <param name="completable"></param>
        public static void ShowLoadingWindow(ICompletable completable) {
            showLoadingWindow?.Invoke(completable);
        }
    }
}
