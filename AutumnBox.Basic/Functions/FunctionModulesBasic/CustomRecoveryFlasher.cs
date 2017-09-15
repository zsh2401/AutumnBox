using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    public sealed class CustomRecoveryFlasher : FunctionModule
    {
        private FileArgs args;
        public CustomRecoveryFlasher(FileArgs fileArg) 
        {
            this.args = fileArg;
        }
        protected override OutErrorData MainMethod()
        {
            Logger.D(TAG, "Start MainMethod");
            var output = executer.Execute(DeviceID, $"flash recovery  \"{args.files[0]}\"");
            executer.Execute(DeviceID, $"boot \"{args.files[0]}\"");
            return output;
        }
    }
}
