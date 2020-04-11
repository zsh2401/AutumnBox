using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services.Impl
{
    class MessageBusImpl : IMessageBus
    {
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        public void SendMessage(string msg_type, object value)
        {
            MessageReceived?.Invoke(this,
                new MessageReceivedEventArgs(value));
        }
    }
}
