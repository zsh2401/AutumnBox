using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Services
{
    interface ISubWindowDialogManager
    {
        Task<object> ShowDialog(string token, ISubWindowDialog dialog);
    }
}
