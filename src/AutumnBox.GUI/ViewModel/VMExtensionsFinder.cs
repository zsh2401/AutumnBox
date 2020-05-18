using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;

using AutumnBox.Leafx.Container;
using AutumnBox.OpenFramework.Management;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using AutumnBox.OpenFramework.Management.ExtTask;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtInfo;
using AutumnBox.OpenFramework.Exceptions;
using AutumnBox.GUI.Util;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.Logging;

namespace AutumnBox.GUI.ViewModel
{
    class VMExtensionsFinder : ViewModelBase
    {
        public IEnumerable<ExtensionDock> Docks
        {
            get => _docks; set
            {
                _docks = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<ExtensionDock> _docks;

        public ExtensionDock SelectedDock
        {
            get => _selectedDock; set
            {
                _selectedDock = value;
                RaisePropertyChanged();
            }
        }
        private ExtensionDock _selectedDock;

        public ICommand ClickItem
        {
            get => _clickItem; set
            {
                _clickItem = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _clickItem;

        public ICommand DoubleClickItem
        {
            get => _doubleClickItem; set
            {
                _doubleClickItem = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _doubleClickItem;

        [AutoInject]
        private ILanguageManager LanguageManager { get; set; }

        [AutoInject]
        private readonly IAdbDevicesManager adbDevicesManager;

        [AutoInject]
        private readonly IOpenFxManager openFxManager;

        [AutoInject]
        private readonly IMessageBus messageBus;

        [AutoInject]
        private readonly INotificationManager notificationManager;

        public VMExtensionsFinder()
        {
            if (IsDesignMode())
            {
                return;
            }
            InitCommand();
            RaisePropertyChangedOnUIThread = true;
            openFxManager.WakeIfLoaded(() =>
            {
                Load();
                messageBus.MessageReceived += (s, e) =>
                {
                    if (e.MessageType == Messages.REFRESH_EXTENSIONS_VIEW)
                    {
                        Load();
                    }
                };
                LanguageManager.LanguageChanged += (s, e) => Load();
                adbDevicesManager.DeviceSelectionChanged += (s, e) => Order();
            });
        }

        private void Load()
        {
            var libsManager = App.Current.Lake.Get<ILibsManager>();
            var devManager = App.Current.Lake.Get<IAdbDevicesManager>();
            Docks = from dock in libsManager.GetAllExtensions().ToDocks()
                    where !dock.ExtensionInfo.Hidden()
                    where (!dock.ExtensionInfo.DeveloperMode()) || Settings.Default.DeveloperMode
                    where (!dock.ExtensionInfo.Regions().Any()) || dock.ExtensionInfo.Regions().Contains(LanguageManager.Current.LanCode)
                    select dock;
            Order();
        }

        private void Order()
        {
            Docks = from dock in Docks
                    orderby dock.ExtensionInfo.Priority() descending
                    orderby dock.Execute.CanExecuteProp descending
                    select dock;
        }

        private void InitCommand()
        {
            ClickItem = new FlexiableCommand((p) =>
            {
                if (!Settings.Default.DoubleClickRunExt)
                {
                    StartExtension((p as ExtensionDock)?.ExtensionInfo);
                }
            });
            DoubleClickItem = new FlexiableCommand((p) =>
            {
                if (Settings.Default.DoubleClickRunExt)
                {
                    StartExtension((p as ExtensionDock)?.ExtensionInfo);
                }
            });
        }

        private void StartExtension(IExtensionInfo inf)
        {
            try
            {
                this.GetComponent<IExtensionTaskManager>().Start(inf);
            }
            catch (DeviceStateIsNotCorrectException)
            {
                notificationManager.Warn("IS NOT TARGET STATE ERROR");
            }
        }
    }
}
