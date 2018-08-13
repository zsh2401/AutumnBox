using System;

namespace AutumnBox.GUI.UI
{
    internal interface IAsyncRefreshable:IExtPanel
    {
        event EventHandler RefreshStart;
        event EventHandler RefreshFinished;
    }
}
