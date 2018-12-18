using AutumnBox.Reporter.Model;
using AutumnBox.Reporter.MVVM;
using AutumnBox.Reporter.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Reporter.ViewModel
{
    class VMUploader : ViewModelBase
    {
        public string Status
        {
            get => _status; set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }
        private string _status;

        public int CountOfUploaded
        {
            get => _countOfUploaded; set
            {
                _countOfUploaded = value;
                RaisePropertyChanged();
            }
        }
        private int _countOfUploaded;

        public int CountOfTotal
        {
            get => _countOfTotal; set
            {
                _countOfTotal = value;
                RaisePropertyChanged();
            }
        }
        private int _countOfTotal;

        public double Progress
        {
            get => _progress; set
            {
                _progress = value;
                RaisePropertyChanged();
            }
        }
        private double _progress = 0.0;

        public VMUploader()
        {
            RaisePropertyChangedOnDispatcher = true;
        }

        private Thread executingThread;

        public void StartUpload(ReportHeader header, IEnumerable<Log> logs)
        {
            executingThread = new Thread(() =>
            {
                Status = "正在传输";
                CountOfTotal = logs.Count();
                new Uploader(header).Upload(logs, (e) =>
                {
                    CountOfUploaded++;
                    Progress = CountOfUploaded / CountOfTotal * 100;
                    if (CountOfUploaded == CountOfTotal)
                    {
                        Status = "传输完毕";
                    }
                });
            });
            executingThread.Start();
        }

        public void Stop()
        {
            executingThread?.Abort();
        }
    }
}
