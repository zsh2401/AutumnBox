using AutumnBox.Basic.AdbEnc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions.ExecutedResultHandler
{
    public abstract class Handler
    {
        public string Message { get { return _message; } protected set { _message = value; } }
        public bool FuncIsSuccess { get { return _funcIsSuccess; } protected set { _funcIsSuccess = value; } }
        public OutputData OutputData { get { return _outputData; } protected set { _outputData = value; } }
        private string _message = "";
        private bool _funcIsSuccess = true;
        private OutputData _outputData = new OutputData();
        internal protected Handler(OutputData o) {
            OutputData = o;
            HandleMethod(OutputData);
        }
        public abstract void HandleMethod(OutputData outputData);
    }
}
