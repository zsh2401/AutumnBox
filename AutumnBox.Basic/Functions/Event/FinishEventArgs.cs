using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Functions.ExecutedResultHandler;
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public class FinishEventArgs:EventArgs
    {
        public OutputData OutputData { get; internal set; }
        public bool IsFinish { get; internal set; }
        public Handler Handler { get; internal set; }
    }
}