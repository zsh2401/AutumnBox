using System;

namespace AutumnBox.GUI.Services
{
    interface IAppLifecycleManager
    {
        event EventHandler AppLoaded;
        bool IsAppLoaded { get; }
        void LoadApplication();
        void UnloadApplication();
    }
}
