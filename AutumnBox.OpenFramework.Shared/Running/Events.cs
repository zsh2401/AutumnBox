using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Running
{
    /// <summary>
    /// 线程结束事件参数
    /// </summary>
    public class ThreadFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="thread"></param>
       public ThreadFinishedEventArgs(IExtensionThread thread)
        {
            Thread = thread ?? throw new ArgumentNullException(nameof(thread));
        }
        /// <summary>
        /// 完成的线程
        /// </summary>
        public IExtensionThread Thread { get; }
    }
    /// <summary>
    /// 线程开始执行的参数
    /// </summary>
    public class ThreadStartedEventArgs : EventArgs { }
}
