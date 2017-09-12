using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Util;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    public sealed class CustomRecoveryFlasher : FunctionModule
    {
        private FileArgs args;
        public CustomRecoveryFlasher(FileArgs fileArg) : base(FunctionArgs.ExecuterInitType.Fastboot)
        {
            this.args = fileArg;
        }
        protected override OutputData MainMethod()
        {
            Logger.D(TAG, "Start MainMethod");
            var output = MainExecuter.Execute(DeviceID, $"flash recovery  \"{args.files[0]}\"");
            MainExecuter.Execute(DeviceID, $"boot \"{args.files[0]}\"");
            return output;
        }
    }
}
