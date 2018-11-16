using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.OpenFramework.Management;
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

        public VMLibsView()
        {
            if (OpenFxObserver.Instance.IsLoaded)
            {
                Load();
            }
            else
            {
                OpenFxObserver.Instance.Loaded += (s, e) =>
                {
                    Load();
                };
            }
        }

        public void Load()
        {
            Libs = LibDock.From(Manager.InternalManager.Librarians);
        }
    }
}
