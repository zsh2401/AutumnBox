using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(object value)
        {
            Value = value;
        }

        public object Value { get; }
    }
    interface IMessageBus
    {
        event EventHandler MessageReceived;
        void SendMessage(string msg_type, object value);
    }
}
