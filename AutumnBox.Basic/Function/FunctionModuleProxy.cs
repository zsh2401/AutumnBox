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
    public class FunctionModuleProxy<T> where T : IFunctionModule, new()
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
        public Type FunctionObjType { get { return typeof(T); } }
        private IFunctionModule FunctionModule;
        public FunctionModuleProxy(ModuleArgs args)
        {
            FunctionModule = new T
            {
                Args = args
            };
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
    }
}
