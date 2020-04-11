using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface INotificationManager
    {
        Task<bool?> SendYN(string msg, string btnYes = null, string btnNo = null);
        Task SendWarn(string msg);
        Task SendInfo(string msg);
    }
}
