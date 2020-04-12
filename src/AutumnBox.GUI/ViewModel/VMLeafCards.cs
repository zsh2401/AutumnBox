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
using AutumnBox.GUI.Services;
using AutumnBox.GUI.Util.Bus;
using AutumnBox.Leafx.ObjectManagement;
using System.Collections.ObjectModel;
namespace AutumnBox.GUI.ViewModel
{
    class VMLeafCards : ViewModelBase
    {
        [AutoInject]
        private readonly ILeafCardManager leafCardManager;

        public ObservableCollection<object> Views => leafCardManager.Views;
    }
}
