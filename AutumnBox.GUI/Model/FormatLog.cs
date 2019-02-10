using System;
using System.Windows.Media;
using AutumnBox.GUI.MVVM;
using AutumnBox.Logging.Management;

namespace AutumnBox.GUI.Model
{
    class FormatLog : ModelBase, ILog
    {
        private readonly ILog log;

        public DateTime Time => log.Time;

        public string TimeString => Time.ToString("yy-MM-dd HH:mm:ss");
        public string LogWindowTimeString => Time.ToString("HH:mm:ss");
        public Color LevelColor
        {
            get
            {
                switch (Level)
                {
                    case "Debug":
                        return Colors.Green;
                    case "Info":
                        return Color.FromRgb(34, 96, 0xDC);
                    case "Exception":
                        return Colors.Red;
                    case "Warn":
                        return Colors.Orange;
                    default:
                        return Colors.Gray;
                }
            }
        }

        public string Level => log.Level;

        public string Category => log.Category;

        public string Message => log.Message;

        public const string FMT = "[{0}][{2}/{1}]:{3}";
        public string Formated
        {
            get
            {
                return string.Format(FMT, TimeString, log.Level, log.Category, log.Message);
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
