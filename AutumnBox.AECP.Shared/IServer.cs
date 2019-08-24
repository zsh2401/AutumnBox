using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.AECP
{
    public interface IServer
    {
        void Send();
        string Receive();
    }
}
