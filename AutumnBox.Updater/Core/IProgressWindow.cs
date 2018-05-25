using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutumnBox.Updater.Core
{
    public  interface IProgressWindow
    {
        void SetUpdateContent(string text);
        void AppendLog(string text);
        void AppendLog(string text, double value);
        void SetProgress(double value);
        void Finish();
    }
}
