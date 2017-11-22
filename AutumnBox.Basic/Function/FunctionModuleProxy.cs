/* =============================================================================*\
*
* Filename: FunctionModuleProxy
* Description: 
*
* Version: 1.0
* Created: 2017/10/23 12:07:07 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;
using AutumnBox.Support.CstmDebug;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function
{
    [LogProperty(TAG = "FMP")]
    /// <summary>
    /// 功能模块代理器,更加方便的管理功能模块
    /// </summary>
    public sealed class FunctionModuleProxy : IOutSender
    {
        private uint registeredCount = 0;
        public event OutputReceivedEventHandler OutputReceived
        {
            add { FunctionModule.OutputReceived += value; }
            remove { FunctionModule.OutputReceived -= value; }
        }
        public event StartupEventHandler Startup
        {
            add { FunctionModule.Startup += value; }
            remove { FunctionModule.Startup -= value; }
        }
        public event FinishedEventHandler Finished
        {
            add { FunctionModule.Finished += value; registeredCount++; }
            remove { FunctionModule.Finished -= value; registeredCount--; }
        }
        public Type FunctionModuleType { get { return FunctionModule.GetType(); } }
        /// <summary>
        /// 代理的模块
        /// </summary>
        private IFunctionModule FunctionModule { get; set; }
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
        public async void AsyncRun(bool enableEventRegisteredCheck = true)
        {
            if (enableEventRegisteredCheck && registeredCount == 0) throw new EventNotBoundException();
            if (!(FunctionModule.Status == ModuleStatus.Ready)) throw new Exception("FM not ready");
            await Task.Run(() =>
            {
                FunctionModule.Run();
            });
        }
        /// <summary>
        /// 同步运行
        /// </summary>
        /// <returns></returns>
        public ExecuteResult SyncRun()
        {
            if (!(FunctionModule.Status == ModuleStatus.Ready)) throw new Exception("FM not ready");
            ExecuteResult result = null;
            FunctionModule.Finished += (s, e) =>
            {
                result = e.Result;
            };
            FunctionModule.Run();
            return result;
        }
        /// <summary>
        /// 强制停止被代理的模块
        /// </summary>
        public void ForceStop()
        {
            FunctionModule.ForceStop();
        }

        /// <summary>
        /// 创建一个新的模块与代理器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        /// <returns></returns>
        public static FunctionModuleProxy Create<T>(ModuleArgs args) where T : IFunctionModule, new()
        {
            FunctionModuleProxy fmp = new FunctionModuleProxy()
            {
                FunctionModule = new T()
            };
            fmp.FunctionModule.Args = args;
            return fmp;
        }
        /// <summary>
        /// 创建一个新的模块与代理器
        /// </summary>
        /// <param name="functionModule"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static FunctionModuleProxy Create(Type fmType, ModuleArgs args)
        {
            if (!(typeof(IFunctionModule).IsAssignableFrom(fmType)))
            {
                throw new ArgumentException("fmType is not implements IFunctionModule", "fmType");
            }
            FunctionModuleProxy fmp = new FunctionModuleProxy()
            {
                FunctionModule = (IFunctionModule)Activator.CreateInstance(fmType)
            };
            fmp.FunctionModule.Args = args;
            return fmp;
        }
    }
}

