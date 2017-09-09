using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    public sealed class OutRecevier:BaseObject
    {
        private FunctionModule fm;
        public event DataReceivedEventHandler OutReceived;
        public event DataReceivedEventHandler ErrorReceived;
        internal OutRecevier(ICanGetRealTimeOut functionModule) {
            functionModule.ErrorDataReceived += ErrorReceived;
            functionModule.OutputDataReceived += OutReceived;
        }
    }
}
