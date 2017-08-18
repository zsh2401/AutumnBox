using AutumnBox.Basic.AdbEnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic
{
    public static class EventsHandlers
    {
        public delegate void FinishEventHandler(OutputData _out);
        public delegate void SimpleFinishEventHandler();
        public delegate void AdvanceFinishEventHandler(object sender, OutputData _out);
    }
}
