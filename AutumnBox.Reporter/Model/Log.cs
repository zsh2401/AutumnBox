using AutumnBox.Reporter.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutumnBox.Reporter.Model
{
    class Log : ModelBase
    {
        public bool NeedUpload
        {
            get => _needUpload;
            set
            {
                _needUpload = value;
                RaisePropertyChanged();
            }
        }
        private bool _needUpload = true;

        public string LogName { get => LogFile.Name; }

        public string Content
        {
            get
            {
                if (_content == null)
                {
                    _content = ReadContent();
                }
                return _content;
            }
        }
        private string _content;

        public FileInfo LogFile { get; }

        public Log(FileInfo logFile)
        {
            LogFile = logFile;
        }

        private string ReadContent()
        {
            return File.ReadAllText(LogFile.FullName);
        }
    }
}
