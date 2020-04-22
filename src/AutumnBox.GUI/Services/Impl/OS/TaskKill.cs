using AutumnBox.Basic.Calling;
using AutumnBox.Basic.ManagedAdb.CommandDriven;

namespace AutumnBox.GUI.Services.Impl.OS
{
    static class TaskKill
    {
        public static void Kill(string exeName)
        {
            using var cmd = new CommandProcedure("cmd.exe", "/c", $"taskkill /F /IM {exeName} /T");
            cmd.Execute();
        }
    }
}
