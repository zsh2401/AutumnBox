using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string type,object value)
        {
            Value = value;
            MessageType = type;
        }

        public object Value { get; }
        public string MessageType { get; }
    }
    interface IMessageBus
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        void SendMessage(string msg_type, object value = null);
    }
}
