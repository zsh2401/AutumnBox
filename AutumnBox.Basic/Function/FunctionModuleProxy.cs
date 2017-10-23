using AutumnBox.Basic.Devices;
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
        /// <summary>
        /// 代理的模块
        /// </summary>
        public IFunctionModule FunctionModule { get; private set; }
        /// <summary>
        /// 代理的模块的状态
        /// </summary>
        public ModuleStatus ModuleStatus { get { return FunctionModule.Status; } }
        /// <summary>
        /// 私有构造
        /// </summary>
        private FunctionModuleProxy() { }
        /// <summary>
        /// 异步运行
        /// </summary>
        public void AsyncRun()
        {
            FunctionModule.AsyncRun();
        }
        /// <summary>
        /// 同步运行
        /// </summary>
        /// <returns></returns>
        public ExecuteResult SyncRun()
        {
            return FunctionModule.SyncRun();
        }
        /// <summary>
        /// 强制停止被代理的模块
        /// </summary>
        public void ForceStop()
        {
            FunctionModule.KillProcess();
        }
        /// <summary>
        /// 创建一个新的模块与代理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
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

