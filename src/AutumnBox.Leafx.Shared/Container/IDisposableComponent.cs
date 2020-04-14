using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Leafx.Container
{
    interface IDisposableComponent : IDisposable
    {
        event EventHandler RequiringDispose;
    }
}
