using AutumnBox.Basic.Executer;
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public class FinishEventArgs:EventArgs
    {
        public OutErrorData OutErrorData { get; internal set; }
        
        public bool IsFinish { get; internal set; }
    }
}