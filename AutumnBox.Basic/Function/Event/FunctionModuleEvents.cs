using AutumnBox.Basic.Executer;
using System;

namespace AutumnBox.Basic.Function.Event
{
    public delegate void StartupEventHandler(object sender, StartupEventArgs e);
    public delegate void FinishedEventHandler(object sender, FinishEventArgs e);
    public class StartupEventArgs : EventArgs
    {
    }
    /// <summary>
    /// 功能完成执行的事件参数
    /// </summary>
    public class FinishEventArgs : EventArgs
    {
        public OutputData OutputData { get { return Result.OutputData; } }
        public ExecuteResult Result { get; internal set; }
        public Object Other { get; internal set; }
    }
}
