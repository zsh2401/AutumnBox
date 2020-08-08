using System;
using System.IO;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Open.LKit
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
        /// LeafUI高度
        /// </summary>
        int Height { get; set; }

        /// <summary>
        /// LeafUI宽度
        /// </summary>
        int Width { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        string StatusInfo { get; set; }

        /// <summary>
        /// 状态描述
        /// </summary>
        string StatusDescription { get; set; }

        /// <summary>
        /// 进度条
        /// </summary>
        int Progress { get; set; }

        /// <summary>
        /// 请求退出时发生
        /// </summary>
        event LeafUIClosingEventHandler Closing;

        #region 生命周期函数
        /// <summary>
        /// 显示界面
        /// </summary>
        void Show();

        /// <summary>
        /// 以Dialog的方式显示在秋之盒之上
        /// </summary>
        void ShowDialog();

        /// <summary>
        /// 向界面传达完成的消息
        /// </summary>
        /// <param name="statusMessage">完成状态信息</param>
        void Finish(string statusMessage = default);

        /// <summary>
        /// 界面将被强行关闭,没有任何提示与过程,不建议在正常情况下使用
        /// </summary>
        void Shutdown();

        /// <summary>
        /// 想详情写入一行
        /// </summary>
        /// <param name="_str"></param>
        void WriteLineToDetails(string _str);

        /// <summary>
        /// 向详情写入信息
        /// </summary>
        /// <param name="_str"></param>
        void WriteToDetails(string _str);

        /// <summary>
        /// WriteLineToDetails换个名字
        /// </summary>
        /// <param name="line"></param>
        void Println(string line);

        /// <summary>
        /// WriteToDetails换个名字
        /// </summary>
        /// <param name="_str"></param>
        void Print(string _str);
        #endregion

        /// <summary>
        /// 开启帮助按钮
        /// </summary>
        /// <param name="callback"></param>
        void EnableHelpBtn(Action callback);

        /// <summary>
        /// 在UI线程中执行代码
        /// </summary>
        /// <param name="act"></param>
        void RunOnUIThread(Action act);

        #region 增强交互API
        /// <summary>
        /// 显示Leaf窗口内的交互Dialog
        /// </summary>
        /// <param name="dialog"></param>
        Task<object> ShowLeafDialog(ILeafDialog dialog);

        /// <summary>
        /// 让用户输入一些字符串
        /// </summary>
        /// <param name="hint"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        string InputString(string hint, string _default = default);

        /// <summary>
        /// 显示一条信息,并阻塞至用户点击确认
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <returns></returns>
        void ShowMessage(string message);

        /// <summary>
        /// 让用户对一条信息进行确认,可自定义按钮
        /// </summary>
        /// <param name="message"></param>
        /// <param name="btnYes"></param>
        /// <param name="btnNo"></param>
        /// <returns></returns>
        bool DoYN(string message, string btnYes = null, string btnNo = null);

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
        /// <param name="hint">简要提示,传null为默认</param>
        /// <returns>被选择的那个对象,如果用户取消选择,返回为null</returns>
        object SelectOne(string hint, params object[] options);
        #endregion
    }
}
