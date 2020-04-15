using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Service
{
    public interface IMessage
    {
        object Content { get; }
        object Result { get; }
    }
}
