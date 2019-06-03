using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Data
{
    public class CommandExecutedEventArgs : EventArgs
    {
        TimeSpan UsedTime { get; }
        ICommandResult Result { get; }
        public string FileName { get; }
        public string Args { get; }
        public TimeSpan Span { get; }

        public CommandExecutedEventArgs(string fileName, string args, ICommandResult result, TimeSpan span)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("message", nameof(fileName));
            }

            FileName = fileName;
            Args = args;
            Span = span;
        }
    }
}
