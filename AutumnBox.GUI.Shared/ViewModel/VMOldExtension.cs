using AutumnBox.Basic.Device;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.OpenFramework.Extension;
using AutumnBox.OpenFramework.Wrapper;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMOldExtension : ViewModelBase
    {
        public IExtensionWrapper Wrapper
        {
            get
            {
                return _wrapper;
            }
            set
            {
                InitEvent();
                _wrapper = value;
                RaisePropertyChanged();
            }
        }
        private IExtensionWrapper _wrapper;

        public FlexiableCommand Execute { get; }
        public VMOldExtension()
        {
            Execute = new FlexiableCommand(p => _Execute());
        }
        private void InitEvent()
        {
            DeviceSelectionObserver.Instance.SelectedDevice += (s, e) => SelectDevice();
            DeviceSelectionObserver.Instance.SelectedNoDevice += (s, e) => SelecteNoDevice();
            if (DeviceSelectionObserver.Instance.IsSelectedDevice)
                SelectDevice();
            else
                SelecteNoDevice();
        }
        private void SelecteNoDevice()
        {
            if (Wrapper.Info.RequiredDeviceStates == AutumnBoxExtension.NoMatter)
            {
                Execute.CanExecuteProp = true;
            }
            else
            {
                Execute.CanExecuteProp = false;
            }
        }
        private void _Execute()
        {
            Wrapper.GetThread().Start();
        }
        private void SelectDevice()
        {
            DeviceState crtState = DeviceSelectionObserver.Instance.CurrentDevice.State;
            DeviceState reqState = Wrapper.Info.RequiredDeviceStates;
            Execute.CanExecuteProp = reqState.HasFlag(crtState);
        }
    }
}
