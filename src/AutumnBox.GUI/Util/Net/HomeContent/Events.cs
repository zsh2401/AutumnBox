using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.Net.HomeContent
{
    internal delegate void HomeContentRefreshedEventHandler(object sender, HomeContentRefreshedEventArgs e);
    internal delegate void HomeContentRefreshingEventHandler(object sender, HomeContentRefreshingEventArgs e);
    class HomeContentRefreshedEventArgs : EventArgs
    {
        public object NewContent { get; }
        public HomeContentRefreshedEventArgs(object newContent)
        {
            NewContent = newContent ?? throw new ArgumentNullException(nameof(newContent));
        }
    }
    class HomeContentRefreshingEventArgs : EventArgs
    {
    }
}
