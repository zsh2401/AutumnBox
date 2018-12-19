using AutumnBox.Basic.Calling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.GUI.Util.OS
{
    static class TaskKill
    {
        public static void Kill(string exeName)
        {
            new CommandExecutor().Cmd($"taskkill /F /IM {exeName} /T");
        }
    }
}
