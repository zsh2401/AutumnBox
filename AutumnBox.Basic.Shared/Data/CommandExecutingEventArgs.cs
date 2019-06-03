using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Data
{
    public class CommandExecutingEventArgs : EventArgs
    {
        public string FileName { get; }
        public string[] Args { get; }
        public CommandExecutingEventArgs(string fileName, params string[] args)
        {
            FileName = fileName;
            Args = args;
        }
    }
}
