
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Basic.Util;

namespace AutumnBox.GUI.Services.Impl.OS
{
    static class TaskKill
    {
        public static void Kill(string exeName)
        {
            using var cmd = new CommandProcedure()
            {
                FileName = "taskkill",
                Arguments = $"/F /IM {exeName} /T"
            };
            cmd.Execute();
        }
    }
}
