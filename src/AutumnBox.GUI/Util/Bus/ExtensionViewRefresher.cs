using System;

namespace AutumnBox.GUI.Util.Bus
{
    [Obsolete("Use service to instead", true)]
    class ExtensionViewRefresher
    {
        public event EventHandler Refreshing;
        public static ExtensionViewRefresher Instance { get; }
        static ExtensionViewRefresher()
        {
            Instance = new ExtensionViewRefresher();
        }
        private ExtensionViewRefresher() { }
        public void Refresh()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Refreshing?.Invoke(this, new EventArgs());
            });
        }
    }
}
