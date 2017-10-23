using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function.Event
{
    public delegate void SingleFileSendedEventHandler(object sender, SingleFileSendedEventArgs e);
    public class SingleFileSendedEventArgs : EventArgs
    {
        public readonly string FileName;
        public SingleFileSendedEventArgs(string fileName)
        {
            FileName = fileName;
        }
    }
}
