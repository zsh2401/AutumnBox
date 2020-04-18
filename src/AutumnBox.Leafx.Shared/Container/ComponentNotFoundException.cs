#nullable enable
using System;
using System.Runtime.Serialization;

namespace AutumnBox.Leafx.Container
{
    public class ComponentNotFoundException : Exception
    {
        public ComponentNotFoundException()
        {
        }

        public ComponentNotFoundException(string msg = "Component not found") :
            base(msg)
        { }

        public ComponentNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ComponentNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
