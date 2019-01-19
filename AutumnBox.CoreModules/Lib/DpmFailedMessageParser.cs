using AutumnBox.Basic.Calling;

namespace AutumnBox.CoreModules.Lib
{
    internal static class DpmFailedMessageParser
    {
        public static bool TryParse(CommandExecutor.Result result, out string tip, out string message)
        {
            tip = null;
            message = null;
            return false;
        }
    }
}
