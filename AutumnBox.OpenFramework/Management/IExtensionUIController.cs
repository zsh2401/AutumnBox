/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/17 18:16:09 (UTC +8:00)
** desc： ...
*************************************************/

using System;

namespace AutumnBox.OpenFramework.Management
{
    /// <summary>
    /// UI控制器关闭事件参数
    /// </summary>
    public class UIControllerClosingEventArgs : EventArgs
    {
        /// <summary>
        /// 取消
        /// </summary>
        public bool Cancel { get; set; } = false;
    }
    /// <summary>
    /// 拓展模块运行中界面控制器
    /// </summary>
    public interface IExtensionUIController
    {
        /// <summary>
        /// 当UI试图被用户关闭中时发生
        /// </summary>
        event EventHandler<UIControllerClosingEventArgs> Closing;
        /// <summary>
        /// 进度条
        /// </summary>
        double ProgressValue { set; }
        /// <summary>
        /// 简要状态
        /// </summary>
        string Tip { set; }
        /// <summary>
        /// 增加一行输出
        /// </summary>
        /// <param name="msg"></param>
        void AppendLine(string msg);
        /// <summary>
        /// wrapper开始运行了
        /// </summary>
        void OnStart();
        /// <summary>
        /// wrapper结束运行了
        /// </summary>
        void OnFinish();
    }
}
