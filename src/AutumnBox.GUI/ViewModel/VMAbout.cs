using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AutumnBox.GUI.ViewModel
{
    class VMAbout : ViewModelBase
    {
        public ICommand UpdateCheck { get; }
        public VMAbout()
        {
            UpdateCheck = new MVVMCommand(P => Updater.Do());
        }
    }
}
