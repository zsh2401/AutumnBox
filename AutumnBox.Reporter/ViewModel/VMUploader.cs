using AutumnBox.Reporter.Model;
using AutumnBox.Reporter.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Reporter.ViewModel
{
    class VMUploader : ViewModelBase
    {
        private const string API = "https://atmb.xxwhite.com/api/sublog";


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
                UploadInnerMethod(header, logs);
            });
            executingThread.Start();
        }
        private void UploadInnerMethod(ReportHeader header, IEnumerable<Log> logs)
        {
            Status = "传输中";
            CountOfTotal = logs.Count();
            foreach (var log in logs)
            {
                Upload(header, log);
                CountOfUploaded++;
                Progress = CountOfUploaded / CountOfTotal;
            }
            Status = "传输完毕";
            executingThread = null;
        }
        private void Upload(ReportHeader header, Log log)
        {
            WebRequest request = WebRequest.CreateHttp(API);
            request.Method = "POST";
            request.Headers["Id"] = header.UUID;
            request.Headers["UserName"] = header.UserName ?? "";
            request.Headers["UserMail"] = header.UserMail ?? "";
            request.Headers["Remark"] = header.Remark ?? "";
            request.Headers["LogName"] = log.LogName;
            request.ContentType = "text";
            byte[] data = Encoding.UTF8.GetBytes(log.Content);
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            using (var response = request.GetResponse())
            {
            }
        }
        public void Stop()
        {
            executingThread?.Abort();
        }
    }
}
