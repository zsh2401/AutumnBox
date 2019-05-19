using AutumnBox.GUI.Model;
using AutumnBox.GUI.MVVM;
using AutumnBox.GUI.Util.Debugging;
using AutumnBox.Logging.Management;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.ViewModel
{
    class VMLog : ViewModelBase
    {
        public ObservableCollection<FormatLog> Logs
        {
            get => _logs; set
            {
                _logs = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<FormatLog> _logs;

        public string Content
        {
            get => _sb.ToString(); set
            {
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get => _title; set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }
        private string _title;

        private StringBuilder _sb = new StringBuilder();

        public VMLog()
        {
            RaisePropertyChangedOnDispatcher = true;
            Logs = new ObservableCollection<FormatLog>();
            Title = "AutumnBox Debug Window-Select and Ctrl+C to copy";
            foreach (var log in LoggingStation.Instance.Logs)
            {
                Append((FormatLog)log);
            }
            LoggingStation.Instance.Logging += Instance_Logging;
        }

        private void Instance_Logging(object sender, LogEventArgs e)
        {
            Append((FormatLog)e.Content);
        }

        private void Append(FormatLog log)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Logs.Add(log);
            });
        }
    }
}
