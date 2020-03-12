using AutumnBox.Basic.Device;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.Util.I18N;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.OpenFramework.Management.Wrapper;
using AutumnBox.OpenFramework.Open;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMExtensionsNoCategory : ViewModelBase
    {
        public IEnumerable<ExtensionWrapperDock> Docks
        {
            get => _docks; set
            {
                _docks = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<ExtensionWrapperDock> _docks;

        public ExtensionWrapperDock SelectedDock
        {
            get => _selectedDock; set
            {
                _selectedDock = value;
                RaisePropertyChanged();
            }
        }
        private ExtensionWrapperDock _selectedDock;

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

        public VMExtensionsNoCategory()
        {
            InitCommand();
            RaisePropertyChangedOnDispatcher = true;
            OpenFxEventBus.AfterOpenFxLoaded(() =>
            {
                Load();
                MainWindowBus.ExtensionListRefreshing += (s, e) => Load();
                LanguageManager.Instance.LanguageChanged += (s, e) => Load();
                DeviceSelectionObserver.Instance.SelectedDevice += (s, e) => Order();
                DeviceSelectionObserver.Instance.SelectedNoDevice += (s, e) => Order();
            });
        }

        private void Load()
        {
            Docks = OpenFxLoader.LibsManager.Wrappers()
                    .Region(LanguageManager.Instance.Current.LanCode)
                    .Hide()
                    .Dev(Settings.Default.DeveloperMode)
                    .ToDocks();
            Order();
        }

        private void Order()
        {
            Docks = from dock in Docks
                    orderby dock.Wrapper.Info[ExtensionInformationKeys.PRIORITY] descending
                    orderby dock.Execute.CanExecuteProp descending
                    select dock;
        }
        private void InitCommand()
        {
            ClickItem = new FlexiableCommand((p) =>
            {
                if (!Settings.Default.DoubleClickRunExt)
                {
                    StartExtension((p as ExtensionWrapperDock)?.Wrapper);
                }
            });
            DoubleClickItem = new FlexiableCommand((p) =>
            {
                if (Settings.Default.DoubleClickRunExt)
                {
                    StartExtension((p as ExtensionWrapperDock)?.Wrapper);
                }
            });
        }

        private void StartExtension(IExtensionWrapper extensionWrapper)
        {
            if (extensionWrapper == null) return;
            bool isNMExt = extensionWrapper.Info.RequiredDeviceStates == AutumnBoxExtension.NoMatter;
            bool isSelectingDevice = DeviceSelectionObserver.Instance.IsSelectedDevice;
            IDevice crtDev = DeviceSelectionObserver.Instance.CurrentDevice;
            DeviceState targetStates = extensionWrapper.Info.RequiredDeviceStates;

            if (isNMExt)//如果目标模块无关设备状态,直接执行
            {
                LakeProvider.Lake.Get<ITaskManager>().CreateNewTaskOf(extensionWrapper.ExtensionType).Start();
            }
            else if (isSelectingDevice && targetStates.HasFlag(crtDev.State))//有关设备状态,并且设备状态正确,执行
            {
                LakeProvider.Lake.Get<ITaskManager>().CreateNewTaskOf(extensionWrapper.ExtensionType).Start();
            }
            else//不符合执行条件,警告
                MainWindowBus.Warning("IS NOT TARGET STATE ERROR");
        }
    }
}
