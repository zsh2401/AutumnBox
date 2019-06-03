using System;

namespace AutumnBox.OpenFramework.LeafExtension.Kit
{
    /// <summary>
    /// Leaf UI关闭事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate bool LeafUIClosingEventHandler(object sender, LeafUIClosingEventArgs e);
    /// <summary>
    /// LeafUI的关闭按钮被点击时发生的事件
    /// </summary>
    public class LeafUIClosingEventArgs : EventArgs
    {
    }
}
