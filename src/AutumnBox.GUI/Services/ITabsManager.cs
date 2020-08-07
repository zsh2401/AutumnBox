using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    public interface ITabController
    {
        object View { get; }
        bool OnClosing();
        void OnClosed();
        string Header { get; }
    }
    interface ITabsManager
    {
        ObservableCollection<ITabController> Tabs { get; }
    }
}
