using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.PaidVersion
{
    interface ILoginUX
    {
        Tuple<string, string> WaitForInputFinished();
        void OnLogining();
        void OnLoginFailed(Exception ex);
        void OnLoginSuccessed();
    }
}
