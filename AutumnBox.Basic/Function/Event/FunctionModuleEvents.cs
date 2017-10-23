using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function.Event
{
    public delegate void FinishedEventHandler(object sender, FinishEventArgs e);
    /// <summary>
    /// 功能完成执行的事件参数
    /// </summary>
    public class FinishEventArgs : EventArgs
    {
        public OutputData OutputData { get { return Result.OutputData; } }
        public ExecuteResult Result { get; internal set; }
    }
}
