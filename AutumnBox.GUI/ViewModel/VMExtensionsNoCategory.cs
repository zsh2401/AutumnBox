using AutumnBox.Basic.Device;
using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Properties;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.GUI.View.Controls;
using AutumnBox.Logging;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Fast;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Wrapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            OpenFxEventBus.AfterOpenFxLoaded(Load);
        }

        private void Load()
        {
            Docks = OpenFx.LibsManager.Wrappers()
                    .Region(Util.I18N.LanguageManager.Instance.Current.LanCode)
                    .Hide()
                    .Dev(Settings.Default.DeveloperMode)
                    .ToDocks();
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
                extensionWrapper.GetThread().Start();
            else if (isSelectingDevice && targetStates.HasFlag(crtDev.State))//有关设备状态,并且设备状态正确,执行
                extensionWrapper.GetThread().Start();
            else//不符合执行条件,警告
                MainWindowBus.Warning("IS NOT TARGET STATE ERROR");
        }
    }
}
