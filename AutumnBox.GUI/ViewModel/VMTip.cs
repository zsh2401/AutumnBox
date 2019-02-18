using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;

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
                Handle();
            }
        }
        private Tip _tip;

        public object Content
        {
            get => Content; set
            {
                _content = value;
                RaisePropertyChanged();
            }
        }
        private object _content;

        private void Handle()
        {
            Content = Tip.Content;
        }
    }
}
