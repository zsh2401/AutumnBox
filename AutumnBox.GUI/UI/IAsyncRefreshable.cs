using System;

namespace AutumnBox.GUI.UI
{
    internal interface IAsyncRefreshable:IRefreshable
    {
        event EventHandler RefreshStart;
        event EventHandler RefreshFinished;
    }
}
