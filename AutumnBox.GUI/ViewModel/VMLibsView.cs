using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.OpenFramework.Management;
using AutumnBox.OpenFramework.Management.ExtLibrary;
using System.Collections.Generic;

namespace AutumnBox.GUI.ViewModel
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

        public VMLibsView()
        {
            OpenFxEventBus.AfterOpenFxLoaded(() =>
            {
                Load();
            });
            ShowInformation = new FlexiableCommand((p) =>
            {
                //(p as ILibrarian).ShowInformation();
            });
        }

        public void Load()
        {
            Libs = LibDock.From(OpenFxLoader.LibsManager.Librarians);
        }
    }
}
