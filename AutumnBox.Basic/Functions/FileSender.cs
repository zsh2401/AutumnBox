using AutumnBox.Basic.Arg;
using AutumnBox.Basic.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 文件发送器
    /// </summary>
    class FileSender:Function,IThreadFunctionRunner
    {
        private Thread mainThread;
        public event EventsHandlers.SimpleFinishEventHandler sendAllFinish;
        public event EventsHandlers.SimpleFinishEventHandler sendSingleFinish;
        public FileSender() : base(FunctionInitType.Adb) { }
        public void Run(IArgs args) {
            if (sendAllFinish == null) {
                throw new EventNotBoundException();
            }
            mainThread = new Thread(new ParameterizedThreadStart(_Run));
            mainThread.Name = "File Sender Thread";
            mainThread.Start(args);
        }
        private void _Run(object args) {
            FileArgs _arg = (FileArgs)args;
            foreach (string file in _arg.files) {
                adb.Execute(_arg.deviceID,$" push {file} /sdcard/");
                sendSingleFinish?.Invoke();
            }
            sendAllFinish();
        }
    }
}
