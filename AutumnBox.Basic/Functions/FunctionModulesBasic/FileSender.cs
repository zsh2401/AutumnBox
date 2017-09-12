using AutumnBox.Basic.Functions.Event;
using System.Diagnostics;
using AutumnBox.Basic.Functions.Interface;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 文件发送器
    /// </summary>
    public sealed class FileSender : FunctionModule
    {
        public event SimpleFinishEventHandler sendSingleFinish;
        private FileArgs args;

        public FileSender(FileArgs fileArg) : base() {
            this.args= fileArg;
        }
        protected override void MainMethod()
        {
            string filename;
            string[] x;
            foreach (string filepath in args.files)
            {
                x = filepath.Split('\\');
                filename = x[x.Length - 1];
                MainExecuter.Execute(DeviceID, $"push \"{filepath}\" /sdcard/{filename}");
                sendSingleFinish?.Invoke();
            }
           OnFinish(this, new FinishEventArgs());
        }
    }
}
