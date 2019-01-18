using System;
using System.Drawing;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafExtension使用的UI控制器
    /// </summary>
    public interface ILeafUI : IDisposable
    {
        /// <summary>
        /// 图标
        /// </summary>
        byte[] Icon { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        Size Size { set; get; }
        /// <summary>
        /// 状态描述
        /// </summary>
        string Tip { get; set; }
        /// <summary>
        /// 进度条
        /// </summary>
        double Progress { get; set; }
        /// <summary>
        /// 请求退出时发生
        /// </summary>
        event EventHandler<LeafCloseBtnClickedEventArgs> CloseButtonClicked;

        #region 生命周期函数
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
        #endregion

        /// <summary>
        /// 输出
        /// </summary>
        /// <param name="content"></param>
        void WriteLine(object content);
        /// <summary>
        /// 开启帮助按钮
        /// </summary>
        /// <param name="callback"></param>
        void EnableHelpBtn(Action callback);

        #region Leaf交互API
        /// <summary>
        /// 显示一条信息,并阻塞至用户点击确认
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        void ShowMessage(string message);
        /// <summary>
        /// 让用户对一条信息进行确认
        /// </summary>
        /// <param name="message"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        bool DoYN(string message);
        /// <summary>
        /// 让用户进行选择
        /// </summary>
        /// <param name="message">关于选择的信息</param>
        /// <param name="btnYes">YES按钮的文本,不传则为默认</param>
        /// <param name="btnNo">NO按钮的文本,不传则为默认</param>
        /// <param name="btnCancel">取消按钮的文本,不传则为默认</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns>当用户点击Yes按钮,则返回true,点击No按钮返回false,点击取消按钮返回null</returns>
        bool? DoChoice(string message, string btnYes = null, string btnNo = null, string btnCancel = null);

        /// <summary>
        /// /进行单选
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <param name="options">选项,至少要有一个值</param>
        /// <param name="hint">简要提示,不传为默认</param>
        /// <returns>被选择的那个对象,如果用户取消选择,返回为null</returns>
        object SelectFrom(object[] options,string hint=null);

        /// <summary>
        /// 8.23暂未实现!! 进行选择,将会阻塞至用户完成选择
        /// </summary>
        /// <param name="option">所有选项</param>
        /// <param name="maxSelect">最多可选</param>
        /// <returns>用户选择的所有选项,如果用户取消,则这个数组为null</returns>
        object[] Select(object[] option, int maxSelect = 2);
        #endregion
    }
}
