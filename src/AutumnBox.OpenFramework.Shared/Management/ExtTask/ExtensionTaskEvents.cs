using System;

namespace AutumnBox.OpenFramework.Management.ExtTask
{
    /// <summary>
    /// 线程结束事件参数
    /// </summary>
    public class TaskFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="task"></param>
        public TaskFinishedEventArgs(IExtensionTask task)
        {
            Task = task ?? throw new ArgumentNullException(nameof(task));
        }
        /// <summary>
        /// 完成的线程
        /// </summary>
        public IExtensionTask Task { get; }
    }
    /// <summary>
    /// 线程开始执行的参数
    /// </summary>
    public class TaskStartedEventArgs : EventArgs { }
}
