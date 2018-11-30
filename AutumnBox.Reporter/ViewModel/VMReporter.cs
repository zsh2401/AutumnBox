using AutumnBox.Reporter.Model;
using AutumnBox.Reporter.MVVM;
using AutumnBox.Reporter.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.Reporter.ViewModel
{
    class VMReporter : ViewModelBase
    {
        public double UploadProgress
        {
            get => _uploadProgress; set
            {
                _uploadProgress = value;
                RaisePropertyChanged();
            }
        }
        private double _uploadProgress;

        public Visibility UploadingPanelVisibily
        {
            get => _uploadingPanelVisibily; set
            {
                _uploadingPanelVisibily = value;
                RaisePropertyChanged();
            }
        }
        private Visibility _uploadingPanelVisibily = Visibility.Hidden;

        public ReportHeader Header
        {
            get => _header; set
            {
                _header = value;
                RaisePropertyChanged();
            }
        }
        private ReportHeader _header;

        public IEnumerable<Log> Logs
        {
            get => _logs;
            set
            {
                _logs = value;
                RaisePropertyChanged();
            }
        }
        private IEnumerable<Log> _logs;

        public ICommand Submit
        {
            get => _submit; set
            {
                _submit = value;
                RaisePropertyChanged();
            }
        }
        private ICommand _submit;

        public VMReporter()
        {
            Header = new ReportHeader()
            {
                UUID = Guid.NewGuid().ToString(),
            };
            Submit = new FlexiableCommand(() =>
            {
                UploadingPanelVisibily = Visibility.Visible;
                UploadProgress = 0;
                var uploader = new Uploader(Header);
                int uploaded = 0;
                uploader.Upload(Logs, (ex) =>
                {
                    if (ex != null)
                    {
                        Trace.WriteLine(ex);
                    }
                    UploadProgress = uploaded / Logs.Count() * 100;
                    if (uploaded == Logs.Count())
                    {
                        UploadingPanelVisibily = Visibility.Hidden;
                    }
                });
            });
        }
    }
}
