using AutumnBox.Basic.Calling;

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
