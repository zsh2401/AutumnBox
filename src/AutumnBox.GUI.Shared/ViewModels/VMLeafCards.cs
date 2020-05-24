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

using AutumnBox.Leafx.ObjectManagement;
using System.Collections.ObjectModel;
namespace AutumnBox.GUI.ViewModels
{
    class VMLeafCards : ViewModelBase
    {
        [AutoInject] ILeafCardManager LeafCardManager { get; set; }

        public ObservableCollection<ViewWrapper> Views => LeafCardManager.Views;
    }
}
