using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Loader
{
    class AppLoaderFailedEventArgs : EventArgs
    {
        public AppLoaderFailedEventArgs(Exception e)
        {
            Exception = e;
        }

        public Exception Exception { get; }
    }
}
