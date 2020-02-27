using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.SERT.Runtime.Impl
{
    internal class RecordableDebugImpl : IDebugAPI
    {
        public void log(params string[] args)
        {
            Trace.WriteLine(args);
        }
    }
}
