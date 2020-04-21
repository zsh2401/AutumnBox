#nullable enable
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 可通知其释放完成
    /// </summary>
    public interface INotifyDisposed
    {
        /// <summary>
        /// 当对象被释放后发生
        /// </summary>
        event EventHandler? Disposed;
    }
}
