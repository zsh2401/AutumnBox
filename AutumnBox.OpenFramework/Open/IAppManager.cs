using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 秋之盒主程序API
    /// </summary>
    public interface IAppManager
    {
        /// <summary>
        /// 显示一个选择窗口.并等待用户返回结果
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="msg">内容</param>
        /// <param name="btnLeft">左按钮文本,默认取消</param>
        /// <param name="btnRight">右按钮文本,默认确定</param>
        /// <returns></returns>
        ChoiceBoxResult ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null);
        /// <summary>
        /// 显示消息窗口
        /// </summary>
        /// <param name="title">标题,建议使用模块名称</param>
        /// <param name="msg">信息</param>
        void ShowMessageBox(string title, string msg);
        /// <summary>
        /// 显示加载窗口
        /// </summary>
        void ShowLoadingWindow();
        /// <summary>
        /// 关闭加载窗口
        /// </summary>
        void CloseLoadingWindow();
        /// <summary>
        /// 在UI线程运行方法
        /// </summary>
        /// <param name="act"></param>
        void RunOnUIThread(Action act);
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Window GetMainWindow();
        /// <summary>
        /// 获取界面语言代码 类似zh-CN
        /// </summary>
        /// <returns>语言代码</returns>
        string CurrentLanguageCode { get; }
        /// <summary>
        /// 显示调试窗口
        /// </summary>
        Window CreateDebuggingWindow();
        /// <summary>
        /// 刷新拓展列表
        /// </summary>
        void RefreshExtensionList();
        /// <summary>
        /// 重启AutumnBox
        /// </summary>
        /// <param name="ctx"></param>
        void RestartApp();
        /// <summary>
        /// 以管理员重启秋之盒
        /// </summary>
        void RestartAppAsAdmin();
        /// <summary>
        /// 关闭AutumnBox
        /// </summary>
        void ShutdownApp();
        /// <summary>
        /// 当前是否以管理员权限运行
        /// </summary>
        bool IsRunAsAdmin { get; }
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
        TReturn GetPublicResouce<TReturn>(string key) where TReturn : class;
    }
}
