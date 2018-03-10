using AutumnBox.Basic.FlowFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AutumnBox.OpenFramework.Open.V1
{
    /// <summary>
    /// GUI接口
    /// </summary>
    public interface IGuiApi
    {
        /// <summary>
        /// 显示一个选择窗口.并等待用户返回结果
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        /// <param name="btnLeft">左按钮文本,默认取消</param>
        /// <param name="btnRight">右按钮文本,默认确定</param>
        /// <returns></returns>
        bool? ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null);
        /// <summary>
        /// 显示消息窗口
        /// </summary>
        /// <param name="title">标题,建议使用模块名称</param>
        /// <param name="msg">信息</param>
        void ShowMessageBox(string title,string msg);
        /// <summary>
        /// 显示加载窗口
        /// </summary>
        /// <param name="completable">可被主动停止或可得知停止的类</param>
        void ShowLoadingWindow(ICompletable completable);
        /// <summary>
        /// 主程序的Dispatcher
        /// </summary>
        Dispatcher Dispatcher { get; }
        /// <summary>
        /// 获取公共资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetPublicResouce(string key);
        /// <summary>
        /// 获取公共资源
        /// </summary>
        /// <typeparam name="TReturn"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        TReturn GetPublicResouce<TReturn>(string key) where TReturn:class;
        /// <summary>
        /// 获取界面语言代码 类似zh-CN
        /// </summary>
        /// <returns>语言代码</returns>
        string CurrentLanguageCode { get; }
    }
}
