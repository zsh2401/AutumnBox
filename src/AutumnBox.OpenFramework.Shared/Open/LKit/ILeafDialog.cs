using System;

namespace AutumnBox.OpenFramework.Open.LKit
{
    /// <summary>
    /// 标准Leaf对话框
    /// </summary>
    public interface ILeafDialog
    {
        /// <summary>
        /// 视图内容
        /// </summary>
        object ViewContent { get; }
        /// <summary>
        /// 请求关闭的事件
        /// </summary>
        event EventHandler<DialogClosedEventArgs> RequestedClose;
    }
}
