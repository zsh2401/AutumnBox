using AutumnBox.Leafx.Container.Support;
using AutumnBox.Logging;
using System;
using System.Collections.ObjectModel;

namespace AutumnBox.GUI.Services.Impl
{
    [Component(Type = typeof(ITabsManager))]
    class TabManager : ITabsManager
    {
        public ObservableCollection<ITabController> Tabs { get; } =
            new ObservableCollection<ITabController>();

        public TabManager()
        {
            Tabs.Add(new MyTab());
            Tabs.Add(new MyTab());
        }

        private class MyTab : ITabController
        {
            public object View => "View";

            public string Header => "Test tab";

            public void OnClosed()
            {
                SLogger<MyTab>.Warn("Closed");
            }

            public bool OnClosing()
            {
                return false;
            }
        }
    }
}
