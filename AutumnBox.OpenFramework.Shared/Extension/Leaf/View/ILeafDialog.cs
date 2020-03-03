using System;

namespace AutumnBox.OpenFramework.Extension.Leaf
{
    public interface ILeafDialog
    {
        object ViewContent { get; }
        event EventHandler<DialogClosedEventArgs> Closed;
    }
}
