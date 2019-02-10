using System;
using AutumnBox.GUI.MVVM;
using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Model
{
    class FormatLog : ModelBase, ILog
    {
        private readonly ILog log;

        public DateTime Time => log.Time;

        public string Level => log.Level;

        public string Category => log.Category;

        public string Message => log.Message;

        public const string FMT = "[{0}][{2}/{1}]>{3}";
        public string Formated
        {
            get
            {
                return string.Format(FMT, DateTime.Now.ToString("yy-MM-dd HH:mm:ss"), log.Level, log.Category, log.Message);
            }
        }
        public override string ToString()
        {
            return Formated;
        }
        public FormatLog(ILog log)
        {
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }
    }
}
