using AutumnBox.Basic.Util;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// recovery刷入器
    /// </summary>
    public sealed class CustomRecoveryFlasher: FunctionModule
    {
        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.Fastboot;
        private FileArgs args;
        public CustomRecoveryFlasher(FileArgs fileArg):base(RequiredDeviceStatus) {
            this.args = fileArg;
        }
        protected override void MainMethod() {
            Logger.D(TAG,"Start MainMethod");
            var output =  fastboot.Execute(DeviceID, $"flash recovery  \"{args.files[0]}\"");
            fastboot.Execute(DeviceID, $"boot \"{args.files[0]}\"");
            OnFinish(this,new FinishEventArgs() {  OutputData=output });
        }
    }
}
