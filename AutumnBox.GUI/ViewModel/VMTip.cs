using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.Logging;
using System.Net;

namespace AutumnBox.GUI.ViewModel
{
    class VMTip : ViewModelBase
    {
        public Tip Tip
        {
            get => _tip; set
            {
                _tip = value;
                RaisePropertyChanged();
            }
        }
        private Tip _tip;
    }
}
