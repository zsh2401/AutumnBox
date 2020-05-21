using AutumnBox.GUI.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutumnBox.GUI.Model
{
    class VersionInfo : ModelBase
    {
        public string Version { get; set; }
        public string Time { get; set; }
        public string Content { get; set; }
        public VersionInfo(string version, string time, string content)
        {
            Version = version ?? throw new ArgumentNullException(nameof(version));
            Time = time ?? throw new ArgumentNullException(nameof(time));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }
    }
}
