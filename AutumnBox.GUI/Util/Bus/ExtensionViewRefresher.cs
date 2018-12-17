using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Bus
{
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
