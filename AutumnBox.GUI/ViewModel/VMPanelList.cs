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
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static AutumnBox.GUI.Util.Bus.PanelsManager;

namespace AutumnBox.GUI.ViewModel
{
    class VMPanelList : ViewModelBase
    {
        public ObservableCollection<ViewContainer> Views { get; } = PanelsManager.Views;
    }
}
