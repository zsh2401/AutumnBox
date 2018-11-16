using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Extension
{
    internal static class ExtensionShared
    {
        public readonly static CommandStation CommandStation;
        static ExtensionShared()
        {
            CommandStation = new CommandStation();
        }
        public static CommandStation NullCheckAndGet(this CommandStation cmdStation)
        {
            return cmdStation ?? CommandStation;
        }
    }
}
