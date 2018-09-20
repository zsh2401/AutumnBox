/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/23 19:19:38 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.OpenFramework.Open;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// 基础秋之盒主程序的API
    /// </summary>
    public interface IAutumnBox_GUI
    {
        /// <summary>
        /// 播放成功音效
        /// </summary>
        void PlayOk();
        /// <summary>
        /// 播放失败音效
        /// </summary>
        void PlayErr();
        #region UI
        /// <summary>
        /// 获取控制器
        /// </summary>
        /// <param name="wrapper"></param>
        /// <returns></returns>
        IExtensionUIController GetUIControllerOf(IExtensionWrapper wrapper);
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <returns></returns>
        dynamic GetMainWindow();
        /// <summary>
        /// 获取调试窗口
        /// </summary>
        /// <returns></returns>
        dynamic CreateDebugWindow();
        /// <summary>
        /// 显示消息窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        dynamic CreateMessageWindow(string title, string msg);
        /// <summary>
        /// 创建选择窗口
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="btnLeft"></param>
        /// <param name="btnRight"></param>
        /// <param name="btnCancel"></param>
        /// <returns></returns>
        dynamic CreateChoiceWindow(string msg, string btnLeft = null, string btnRight = null, string btnCancel = null);
        /// <summary>
        /// 获取加载窗口
        /// </summary>
        /// <returns></returns>
        dynamic CreateLoadingWindow();
        #endregion
        /// <summary>
        /// 检测秋之盒是否以管理员模式运行
        /// </summary>
        bool IsRunAsAdmin { get; }
        /// <summary>
        /// 秋之盒版本号
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetResouce(string key);
        /// <summary>
        /// 重启程序
        /// </summary>
        void Restart();
        /// <summary>
        /// 以管理员方式重启程序
        /// </summary>
        void RestartAsAdmin();
        /// <summary>
        /// 关闭程序
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 在UI线程中运行代码
        /// </summary>
        /// <param name="act"></param>
        void RunOnUIThread(Action act);
        /// <summary>
        /// 获取当前语言代码
        /// </summary>
        /// <returns></returns>
        string GetCurrentLanguageCode();
        /// <summary>
        /// Log接口
        /// </summary>
        /// <param name="tagOrSender"></param>
        /// <param name="levelString"></param>
        /// <param name="text"></param>
        void Log(object tagOrSender, string levelString, string text);
    }
}
