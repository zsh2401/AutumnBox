/*

* ==============================================================================
*
* Filename: VMPanelList
* Description: 
*
* Version: 1.0
* Created: 2020/3/14 1:11:30
* Compiler: Visual Studio 2019
*
* Author: zsh2401
*
* ==============================================================================
*/
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Bus;
using System.Collections.ObjectModel;
using static AutumnBox.GUI.Util.Bus.PanelsManager;

namespace AutumnBox.GUI.ViewModel
{
    class VMPanelList : ViewModelBase
    {
        public ObservableCollection<ViewContainer> Views
        {
            get => _view; set
            {
                _view = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<ViewContainer> _view;

        public VMPanelList()
        {
            this.Views = PanelsManager.Instance.Views;
            PanelsManager.Instance.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(PanelsManager.Views))
                {
                    this.Views = PanelsManager.Instance.Views;
                }
            };
        }
    }
}
