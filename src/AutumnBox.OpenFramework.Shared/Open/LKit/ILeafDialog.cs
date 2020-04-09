using System;

namespace AutumnBox.OpenFramework.Open.LKit
{
    public interface ILeafDialog
    {
        object ViewContent { get; }
        event EventHandler<DialogClosedEventArgs> Closed;
    }
}
