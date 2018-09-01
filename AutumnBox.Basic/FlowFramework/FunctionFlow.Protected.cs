/* =============================================================================*\
*
* Filename: FunctionFlow.Protected
* Description: 
*
* Version: 1.0
* Created: 2017/11/23 14:52:17 (UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Executer;
using AutumnBox.Support.Log;
using System;
using System.Threading.Tasks;

namespace AutumnBox.Basic.FlowFramework
{
    partial class FunctionFlow<TArgs, TResult>
    {
        /// <summary>
        /// 当外部调用Init时,内部调用此函数
        /// </summary>
        /// <param name="moduleArgs"></param>
        protected virtual void Initialization(TArgs moduleArgs)
        {
            if (_isInited)
            {
                throw new Exception("Args is setted!");
            }
            _isInited = true;
            _args = moduleArgs;
        }
        /// <summary>
        /// 在初始化后调用,在执行前进行检查,默认返回OK
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        protected virtual CheckResult Check(TArgs args) { return CheckResult.OK; }
        /// <summary>
        /// 在MainMethod执行前调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnStartup(StartupEventArgs e)
        {
            Task.Run(() =>
            {
                Startup?.Invoke(this, e);
            });
        }
        /// <summary>
        /// 功能流程的核心函数,所有的操作都必须在这里完成
        /// </summary>
        /// <param name="toolKit">工具箱</param>
        /// <returns></returns>
        protected abstract Output MainMethod(ToolKit<TArgs> toolKit);
        /// <summary>
        /// 当接收到标准输出或标准错误时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnOutputReceived(OutputReceivedEventArgs e)
        {
            Task.Run(() =>
            {
                OutputReceived?.Invoke(this, e);
            });
        }
        /// <summary>
        /// 当toolKit.Executer中的核心进程启动时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnProcessStarted(ProcessStartedEventArgs e)
        {
            Logger.Info(this,"Process Started,Pid is " + e.Pid.ToString());
            _pid = e.Pid;
            ProcessStarted?.Invoke(this, e);
        }
        /// <summary>
        /// 处理执行结果,在MainMethod执行完成后调用,如有需要,请重写这个函数
        /// </summary>
        /// <param name="result"></param>
        protected virtual void AnalyzeResult(TResult result)
        {
            result.ResultType = isForceStoped ? ResultType.Unsuccessful : result.ResultType;
        }
        /// <summary>
        /// 在AnalyzeResult之后,也就是功能流程正式结束时调用
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnFinished(FinishedEventArgs<TResult> e)
        {
            _resultTmp = e.Result;
            NoArgFinished?.Invoke(this, new EventArgs());
            Finished?.Invoke(this, e);
            if ((Finished == null && !_isSync) || MustTiggerAnyFinishedEvent)
            {
                Logger.Debug(this,"AnyFinished event trigger");
                RisesAnyFinishedEvent(this, new FinishedEventArgs<FlowResult>(e.Result));
            }
        }
    }
}
