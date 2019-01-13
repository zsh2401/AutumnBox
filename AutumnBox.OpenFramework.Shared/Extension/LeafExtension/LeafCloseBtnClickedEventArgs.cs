using System;

namespace AutumnBox.OpenFramework.Extension.LeafExtension
{
    /// <summary>
    /// LeafUI的关闭按钮被点击时发生的事件
    /// </summary>
    public class LeafCloseBtnClickedEventArgs : EventArgs
    {
        /// <summary>
        /// 是否可被关闭
        /// </summary>
        public bool CanBeClosed { get; set; } = false;
    }
}
