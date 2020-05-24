/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/18 19:08:39 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging;
using HandyControl.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModels
{
    class VMPanelMain : ViewModelBase
    {
        public int TabSelectedIndex
        {
            get => tabSelectedIndex;
            set
            {
                tabSelectedIndex = value;
                RaisePropertyChanged();
            }
        }
        private int tabSelectedIndex;

        [AutoInject]
        private readonly ITabsManager tabsManager;

        public ObservableCollection<ITabController> Tabs => tabsManager.Tabs;

        [AutoInject]
        private readonly IAdbDevicesManager adbDevicesManager;

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

        public VMPanelMain()
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
                SLogger<VMPanelMain>.Info($"There is {Tabs.Count()} tab");
            });
            adbDevicesManager.DeviceSelectionChanged += Instance_SelectedDevice;
        }

        private void Instance_SelectedDevice(object sender, EventArgs e)
        {
            TabSelectedIndex = 1;
        }
    }
}
