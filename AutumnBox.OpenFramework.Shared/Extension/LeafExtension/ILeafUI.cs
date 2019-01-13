using System;
using System.Drawing;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafExtension使用的UI控制器
    /// </summary>
    public interface ILeafUI
    {
        /// <summary>
        /// 显示界面
        /// </summary>
        void Show();
        /// <summary>
        /// 完成,进度将被置为完成,Tip将根据返回值决定,并且关闭按钮点击事件将不再被触发
        /// </summary>
        /// <param name="exitCode">传入的返回值</param>
        void Finish(int exitCode = 0);
        /// <summary>
        /// 完成,进度将被置为完成,Tip将会被设置为参数,并且关闭按钮点击事件将不再被触发
        /// </summary>
        /// <param name="tip">需要设置的Tip</param>
        void Finish(string tip);
        /// <summary>
        /// 界面将被强行关闭,没有任何提示与过程,不建议在正常情况下使用
        /// </summary>
        void Shutdown();
        /// <summary>
        /// 进度条
        /// </summary>
        double Progress { get; set; }
        /// <summary>
        /// 状态描述
        /// </summary>
        string Tip { get; set; }
        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="content"></param>
        void WriteLine(object content);
        /// <summary>
        /// 请求退出时发生
        /// </summary>
        event EventHandler<LeafCloseBtnClickedEventArgs> CloseButtonClicked;
        /// <summary>
        /// 开启帮助按钮
        /// </summary>
        /// <param name="callback"></param>
        void EnableHelpBtn(Action callback);
        /// <summary>
        /// 大小
        /// </summary>
        Size Size { get; }
    }
}
