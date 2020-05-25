using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.ViewModels
{
    class VMLog : ViewModelBase
    {
        public ILogsCollection Logs => loggingManager.Logs;

        public string Title
        {
            get => _title; set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        [AutoInject]
        private ILoggingManager loggingManager;

        public VMLog()
        {
            RaisePropertyChangedOnUIThread = true;
            Title = "AutumnBox Debug Window-Select and Ctrl+C to copy";
        }
    }
}
