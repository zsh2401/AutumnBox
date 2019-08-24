using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.AECP
{
    public interface IClient
    {
        void Send();
        string Receive();
    }
}
