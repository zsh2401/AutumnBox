using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.ExecutedResultHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions.Event
{
    /// <summary>
    /// 功能完成执行的事件参数
    /// </summary>
    public class FinishEventArgs : EventArgs
    {
        public OutputData OutErrorData { get; internal set; }

        public bool IsFinish { get; internal set; }
    }
    /// <summary>
    /// 运行时管理器完成时的事件参数
    /// </summary>
    public class RMFinishEventArgs : EventArgs
    {
        ExecuteResult ExecuteResult;
    }
    /// <summary>
    /// 功能模块开始执行时的事件参数
    /// </summary>
    public class StartEventArgs : EventArgs
    {
        public IArgs Args { get; set; }
    }
}
