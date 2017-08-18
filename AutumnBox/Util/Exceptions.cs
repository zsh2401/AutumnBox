using System;
using System.Runtime.Serialization;

namespace AutumnBox.Util
{
    internal class ExecuteCommandException : Exception, ISerializable
    {
        public ExecuteCommandException(string message,Exception e) 
            :base(message:message,innerException:e){}
    }
    internal class SetValueException : Exception, ISerializable
    {
        public SetValueException(string message) : base(message) { }
    }
    internal class NotSetEventHandlerException : Exception,ISerializable {
        public NotSetEventHandlerException(string message):base(message){ }
    }
}
