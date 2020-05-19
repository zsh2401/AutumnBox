using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface INotificationManager
    {
        string Token { get; set; }
        Task<bool> Ask(string msg);
        void Warn(string msg);
        void Info(string msg);
        void Success(string msg);
    }
}
