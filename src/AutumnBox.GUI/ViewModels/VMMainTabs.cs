using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using HandyControl.Data;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModels
{
    class VMMainTabs : ViewModelBase
    {
        [AutoInject]
        private readonly ITabsManager tabsManager;
        public ObservableCollection<ITabController> Tabs => tabsManager.Tabs;

        public ICommand ClosingTab
        {
            get
            {
                return _closingTab;
            }
            set
            {
                _closingTab = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _closingTab;

        public ICommand TabClosed
        {
            get
            {
                return _tabClosed;
            }
            set
            {
                _tabClosed = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _tabClosed;

        public VMMainTabs()
        {
            if (IsDesignMode()) return;
            ClosingTab = new MVVMCommand(p =>
            {
                var e = (CancelRoutedEventArgs)p;
                var tabController = (ITabController)e.OriginalSource;
                e.Cancel = !tabController.OnClosing();
            });
            TabClosed = new MVVMCommand(p =>
            {
                var tabController = ((p as RoutedEventArgs).OriginalSource as ITabController);
                tabController?.OnClosed();
            });
        }
    }
}
