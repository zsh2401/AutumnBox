using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions.Interface
{
    public interface IOutReceiver
    {
        void OutReceived(object sender,DataReceivedEventArgs e);
        void ErrorReceived(object sender, DataReceivedEventArgs e);
    }
}
