using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Util
{
    internal class ExecuteCommandException : Exception {
        public ExecuteCommandException(string message,Exception e) 
            :base(message:message,innerException:e){}
    }
    internal class SetValueException : Exception {
        public SetValueException(string message) : base(message) { }
    }
    internal class NotSetEventHandlerException : Exception {
        public NotSetEventHandlerException(string message):base(message){ }
    }
}
