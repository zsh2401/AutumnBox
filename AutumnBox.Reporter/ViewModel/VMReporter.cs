using AutumnBox.Reporter.Model;
using AutumnBox.Reporter.MVVM;
using AutumnBox.Reporter.Util;
using AutumnBox.Reporter.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace AutumnBox.Reporter.ViewModel
{
    class VMReporter : ViewModelBase
    {
        public ReportHeader Header
        {
            get => _header; set
            {
                _header = value;
                RaisePropertyChanged();
            }
        }
        private ReportHeader _header;

        public bool IsSelectAll
        {
            get => _isSelectAll;
            set
            {
                _isSelectAll = value;
                foreach (var log in Logs)
                {
                    log.NeedUpload = value;
                }
                RaisePropertyChanged();
            }
        }
        private bool _isSelectAll = false;

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
            Header = new ReportHeader();
            Submit = new FlexiableCommand(SubmitImpl);
            Logs = LogsScanner.Scan();
            UpdateIsSelectAll();
        }
        private void UpdateIsSelectAll()
        {
            int countOfNeedUpload = Logs.Where((log) =>
            {
                return log.NeedUpload;
            }).Count();
            IsSelectAll = countOfNeedUpload == Logs.Count();
        }
        private void SubmitImpl()
        {
            var needUploads = from log in Logs
                              where log.NeedUpload
                              select log;
            if (needUploads.Count() == 0)
            {
                MessageBox.Show("请至少选择一个Log文件！", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            new UploadingWindow(Header, needUploads).ShowDialog();
        }
    }
}
