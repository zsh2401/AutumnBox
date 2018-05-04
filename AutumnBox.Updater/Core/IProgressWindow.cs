using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace AutumnBox.Updater.Core
{
    interface IProgressWindow
    {
        void SetUpdateContent(string text);
        void SetTip(string text);
        void SetTip(string text, double value);
        void SetTipColor(Color color);
        void SetProgress(double value);
        void Finish();
    }
}
