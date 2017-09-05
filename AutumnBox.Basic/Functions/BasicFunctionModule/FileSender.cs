using AutumnBox.Basic.Util;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 文件发送器
    /// </summary>
    public sealed class FileSender : FunctionModule
    {
        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.RunningOrRecovery;
        public event SimpleFinishEventHandler sendSingleFinish;
        private FileArgs args;
        public FileSender(FileArgs fileArg) : base(RequiredDeviceStatus) {
            this.args= fileArg;
            var d = this.ToString().Split('.');
            TAG = d[d.Length - 1];
        }
        protected override void MainMethod()
        {
            string filename;
            string[] x;
            foreach (string filepath in args.files)
            {
                x = filepath.Split('\\');
                filename = x[x.Length - 1];
                adb.Execute(DeviceID, $"push \"{filepath}\" /sdcard/{filename}");
                sendSingleFinish?.Invoke();
            }
           OnFinish(this, new FinishEventArgs());
        }
    }
}
