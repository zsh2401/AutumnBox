using AutumnBox.Basic.Functions.Event;
using System.Diagnostics;
using AutumnBox.Basic.Functions.Interface;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 文件发送器
    /// </summary>
    public sealed class FileSender : FunctionModule,ICanGetRealTimeOut, IFunctionCanStop
    {
        private static new FunctionRequiredDeviceStatus RequiredDeviceStatus = FunctionRequiredDeviceStatus.RunningOrRecovery;
        public event SimpleFinishEventHandler sendSingleFinish;
        public event DataReceivedEventHandler OutputDataReceived;
        public event DataReceivedEventHandler ErrorDataReceived;

        private FileArgs args;

        public int CmdProcessPID { get { return adb.Pid; } }

        public FileSender(FileArgs fileArg) : base(RequiredDeviceStatus) {
            this.args= fileArg;
            adb.OutputDataReceived += OutputDataReceived;
            adb.ErrorDataReceived += ErrorDataReceived;
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
