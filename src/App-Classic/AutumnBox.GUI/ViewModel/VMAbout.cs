using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMAbout : ViewModelBase
    {
        public ICommand UpdateCheck { get; }

        [AutoInject]
        private IOpenFxManager OpenFxManager { get; set; }

        public VMAbout()
        {
            UpdateCheck = new MVVMCommand((p) => OpenFxManager.RunExtension("EAutumnBoxUpdateChecker"));
        }
    }
}
