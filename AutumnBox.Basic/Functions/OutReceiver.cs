using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    public class OutReceiver
    {
        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;
        internal OutReceiver(ICanGetRealTimeOut function) {
            function.ErrorDataReceived +=   ErrorDataReceived;
            function.OutputDataReceived += OutputDataReceived;
        }
    }
}

