using AutumnBox.Basic.AdbEnc;
using System;

namespace AutumnBox.Basic.Functions.Event
{
    public class FinishEventArgs:EventArgs
    {
        public OutputData OutputData { get; internal set; }
        public bool IsFinish { get; internal set; }
    }
}