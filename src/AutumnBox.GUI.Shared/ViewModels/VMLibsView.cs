using AutumnBox.GUI.MVVM;
using AutumnBox.OpenFramework.Management;
using AutumnBox.Leafx.Container;
using System.Collections.Generic;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using AutumnBox.GUI.Models;

namespace AutumnBox.GUI.ViewModels
{
    class VMLibsView : ViewModelBase
    {
        public LibDock SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                RaisePropertyChanged();
            }
        }
        private LibDock _selectedItem;

        public IEnumerable<LibDock> Libs
        {
            get => _libs; set
            {
                _libs = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<LibDock> _libs;

        public FlexiableCommand ShowInformation
        {
            get => _showInformation; set
            {
                _showInformation = value;
                RaisePropertyChanged();
            }
        }
        private FlexiableCommand _showInformation;

        [AutoInject]
        private readonly IOpenFxManager openFxManager;

        public VMLibsView()
        {
            if (IsDesignMode())
            {
                return;
            }
            openFxManager.WakeIfLoaded(() =>
            {
                Load();
            });
            ShowInformation = new FlexiableCommand((p) =>
            {
                (p as ILibrarian).DisplayMessage();
            });
        }

        public void Load()
        {
            var libsManager = App.Current.Lake.Get<ILibsManager>();
            Libs = LibDock.From(libsManager.Librarians);
        }
    }
}
