using AutumnBox.OpenFramework.Warpper;
using System;
using System.Windows;

namespace AutumnBox.OpenFramework.Open
{
    /// <summary>
    /// 秋之盒主程序API
    /// </summary>
    public interface IAppManager
    {
        /// <summary>
        /// 获取拓展模块的UI控制器
        /// </summary>
        /// <param name="warpper"></param>
        /// <returns></returns>
        IExtensionUIController GetUIControllerOf(IExtensionWarpper warpper);
        /// <summary>
        /// 获取秋之盒版本号
        /// </summary>
        Version Version { get; }
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
        /// 显示加载窗口,这个函数不会阻塞
        /// </summary>
        void ShowLoadingWindow();
        /// <summary>
        /// 关闭加载窗口
        /// </summary>
        void CloseLoadingWindow();
        /// <summary>
        /// 在UI线程运行方法
        /// </summary>
        /// <example>
        /// App.RunOnUIThread(()=>{
        ///     App.ShowMessageBox("Title","Message");
        /// });
        /// </example>
        /// <param name="act"></param>
        void RunOnUIThread(Action act);
        /// <summary>
        /// 获取秋之盒的主要主窗口
        /// </summary>
        /// <returns>秋之盒主窗口</returns>
        Window GetMainWindow();
        /// <summary>
        /// 获取界面语言代码 类似zh-CN
        /// </summary>
        /// <returns>语言代码</returns>
        string CurrentLanguageCode { get; }
        /// <summary>
        /// 获取一个新创建的调试窗口对象
        /// </summary>
        /// <example>
        /// var dbWind = App.CreateDebuggingWindow();
        /// dbWind.ShowDialog();
        /// </example>
        Window CreateDebuggingWindow();
        /// <summary>
        /// 刷新拓展列表
        /// </summary>
        void RefreshExtensionList();
        /// <summary>
        /// 重启AutumnBox
        /// </summary>
        /// <exception cref="Exceptions.UserDeniedException">用户拒绝了该操作</exception>
        void RestartApp();
        /// <summary>
        /// 请求管理员重启秋之盒
        /// </summary>
        /// <exception cref="Exceptions.UserDeniedException">用户拒绝了该操作</exception>
        void RestartAppAsAdmin();
        /// <summary>
        /// 关闭AutumnBox
        /// </summary>
        /// <exception cref="Exceptions.UserDeniedException">用户拒绝了该操作</exception>
        void ShutdownApp();
        /// <summary>
        /// 当前是否以管理员权限运行
        /// </summary>
        bool IsRunAsAdmin { get; }
        /// <summary>
        /// 获取公共资源
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        object GetPublicResouce(string key);
        /// <summary>
        /// 获取公共资源
        /// </summary>
        /// <typeparam name="TReturn">返回类型</typeparam>
        /// <param name="key">键</param>
        /// <returns></returns>
        TReturn GetPublicResouce<TReturn>(string key) where TReturn : class;
    }
}
