using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutumnBox.OpenFramework.Open.Impl.AutumnBoxApi
{
    /// <summary>
    /// AutumnBox原始API
    /// </summary>
    public interface IAutumnBoxGuiApi
    {
        /// <summary>
        /// 秋之盒版本号
        /// </summary>
        Version Version { get; }
        /// <summary>
        /// 获取主窗口
        /// </summary>
        /// <returns></returns>
        Window GetMainWindow();
        /// <summary>
        /// 获取调试窗口
        /// </summary>
        /// <returns></returns>
        Window CreateDebugWindow();
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetResouce(string key);
        /// <summary>
        /// 刷新拓展模块列表
        /// </summary>
        void RefreshExtensionList();
        /// <summary>
        /// 重启程序
        /// </summary>
        void Restart();
        /// <summary>
        /// 以管理员方式重启程序
        /// </summary>
        void RestartAsAdmin();
        /// <summary>
        /// 在UI线程中运行代码
        /// </summary>
        /// <param name="act"></param>
        void RunOnUIThread(Action act);
        /// <summary>
        /// 关闭程序
        /// </summary>
        void Shutdown();
        /// <summary>
        /// 显示消息窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        void ShowMessageBox(string title, string msg);
        /// <summary>
        /// 显示选择窗口
        /// </summary>
        /// <param name="title"></param>
        /// <param name="msg"></param>
        /// <param name="btnLeft"></param>
        /// <param name="btnRight"></param>
        /// <returns></returns>
        ChoiceBoxResult ShowChoiceBox(string title, string msg, string btnLeft = null, string btnRight = null);
        /// <summary>
        /// 获取当前语言代码
        /// </summary>
        /// <returns></returns>
        string GetCurrentLanguageCode();
        /// <summary>
        /// 显示加载窗口
        /// </summary>
        void ShowLoadingWindow();
        /// <summary>
        /// 关闭加载窗口
        /// </summary>
        void CloseLoadingWindow();
    }
}
