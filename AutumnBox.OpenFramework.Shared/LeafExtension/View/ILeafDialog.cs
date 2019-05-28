using System;

namespace AutumnBox.OpenFramework.LeafExtension.View
{
    public interface ILeafDialog
    {
        object ViewContent { get; }
        event EventHandler<DialogClosedEventArgs> Closed;
    }
}
