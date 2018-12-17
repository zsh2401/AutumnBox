using AutumnBox.OpenFramework.Wrapper;
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
        /// 是否是秋之盒开发者模式
        /// </summary>
        bool IsDeveloperMode { get; }
        /// <summary>
        /// 获取秋之盒版本号
        /// </summary>
        Version Version { get; }
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
        dynamic GetMainWindow();
        /// <summary>
        /// 获取界面语言代码 类似zh-CN
        /// </summary>
        /// <returns>语言代码</returns>
        string CurrentLanguageCode { get; }
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
