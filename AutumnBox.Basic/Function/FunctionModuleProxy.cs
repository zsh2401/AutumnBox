using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function
{

    public class FunctionModuleProxy
    {
        public event DataReceivedEventHandler OutReceived
        {
            add { FunctionModule.OutReceived += value; }
            remove { FunctionModule.OutReceived -= value; }
        }
        public event DataReceivedEventHandler ErrorReceived
        {
            add { FunctionModule.ErrorReceived += value; }
            remove { FunctionModule.ErrorReceived -= value; }
        }
        public event EventHandler Startup
        {
            add { FunctionModule.Startup += value; }
            remove { FunctionModule.Startup -= value; }
        }
        public event FinishedEventHandler Finished
        {
            add { FunctionModule.Finished += value; }
            remove { FunctionModule.Finished -= value; }
        }
        public IFunctionModule FunctionModule { get; private set; }
        private FunctionModuleProxy()
        {
        }
        public void AsyncRun() =>
            FunctionModule.AsyncRun();
        public ExecuteResult SyncRun()
        {
            return FunctionModule.SyncRun();
        }
        public void ForceStop()
        {
            FunctionModule.KillProcess();
        }
        public static FunctionModuleProxy Create<T>(ModuleArgs e) where T : IFunctionModule, new()
        {
            FunctionModuleProxy fmp = new FunctionModuleProxy()
            {
                FunctionModule = new T()
            };
            fmp.FunctionModule.Args = e;
            return fmp;
        }
    }
}

