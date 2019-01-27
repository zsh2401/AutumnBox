using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Calling.Cmd;
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
            using (var executor = new CommandExecutor())
            {
                executor.Cmd($"taskkill /F /IM {exeName} /T");
            }
        }
    }
}
