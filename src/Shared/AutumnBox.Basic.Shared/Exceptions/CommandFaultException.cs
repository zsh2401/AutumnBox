using AutumnBox.Basic.Calling;
using System;

namespace AutumnBox.Basic.Exceptions
{
    class CommandFaultException : Exception
    {
        public CommandFaultException(ICommandResult commandResult,string command=null)
        {
            CommandResult = commandResult ?? throw new ArgumentNullException(nameof(commandResult));
            Command = command;
        }
        public string Command { get; set; }

        public ICommandResult CommandResult { get; }
    }
}
