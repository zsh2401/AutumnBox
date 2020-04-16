using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Services;
using AutumnBox.Leafx.ObjectManagement;
using AutumnBox.Logging.Management;
using System.Collections.ObjectModel;
using System.Text;

namespace AutumnBox.GUI.ViewModel
{
    class VMLog : ViewModelBase
    {
        public ObservableCollection<ILog> Logs => loggingManager.CoreLogger.Logs;

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
            RaisePropertyChangedOnDispatcher = true;
            Title = "AutumnBox Debug Window-Select and Ctrl+C to copy";
        }
    }
}
