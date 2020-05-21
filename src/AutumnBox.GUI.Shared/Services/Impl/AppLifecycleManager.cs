using AutumnBox.GUI.Util.Loader;
using AutumnBox.Leafx.Container.Support;
using System;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(IAppLifecycleManager))]
    class AppLifecycleManager : IAppLifecycleManager
    {
        public bool IsAppLoaded => throw new NotImplementedException();

        public event EventHandler AppLoaded
        {
            add
            {
                if (IsAppLoaded)
                {
                    value(this, new EventArgs());
                }
                else
                {
                    appLoadedSource += value;
                }
            }
            remove
            {
                appLoadedSource -= value;
            }
        }
        private event EventHandler appLoadedSource;

        public void LoadApplication()
        {
            //TODO
        }

        public void UnloadApplication()
        {
            //TODO
        }
    }
}
