using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Windows
{
    internal interface IAppLoadingWindow
    {
        void SetProgress(double value);
        void SetTip(string value);
        void Finish();
    }
}
