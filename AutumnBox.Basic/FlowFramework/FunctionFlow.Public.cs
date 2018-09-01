/* =============================================================================*\
*
* Filename: FunctionFlow.Public
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
using AutumnBox.Basic.Executer;
using AutumnBox.Support.Log;
using AutumnBox.Support.Helper;
using System;
using System.Threading.Tasks;
using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.FlowFramework
{
    /// <summary>
    /// 功能流程基类
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract partial class FunctionFlow<TArgs, TResult>
        : FunctionFlowBase, IOutputable, IForceStoppable, IDisposable, ICompletable, IFunctionFlowBase
        where TArgs : FlowArgs, new()
        where TResult : FlowResult, new()
    {
        /// <summary>
        /// 当主流程即将执行时触发
        /// </summary>
        public event StartupEventHandler Startup;
        /// <summary>
        /// 当全部流程结束时触发
        /// </summary>
        public event FinishedEventHandler<TResult> Finished;
        /// <summary>
        /// 当有标准输出产生时触发
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 当流程内部的主要外部进程开始时触发
        /// </summary>
        public event ProcessStartedEventHandler ProcessStarted;
        /// <summary>
        /// 没有任何特殊参数的结束事件
        /// </summary>
        public event EventHandler NoArgFinished;
        /// <summary>
        /// 这个FunctionFlow是否被关闭,只有流程正在进行或结束后为True
        /// </summary>
        public bool IsClosed { get; private set; } = false;
        /// <summary>
        /// 是否必须触发AnyFinished,通常情况下,如果Finished被绑定了,就不会触发AnyFinished
        /// </summary>
        public bool MustTiggerAnyFinishedEvent { get; set; } = false;
        /// <summary>
        /// 构建
        /// </summary>
        public FunctionFlow()
        {
            _executer = new CommandExecuter();
            _resultTmp = new TResult();
            _executer.ProcessStarted += (s, e) =>
            {
                OnProcessStarted(e);
            };
            _executer.OutputReceived += (s, e) =>
            {
                OnOutputReceived(e);
            };
        }
        /// <summary>
        /// 初始化参数,只可以调用一次
        /// </summary>
        /// <param name="args"></param>
        public void Init(TArgs args)
        {
            Initialization(args);
        }
        /// <summary>
        /// 当前Flow是否是同步执行的
        /// </summary>
        private bool _isSync = false;
        /// <summary>
        /// 异步执行这个FunctionFlow
        /// </summary>
        public async void RunAsync()
        {
            Logger.Info(this,"Run async");
            if (_args == null) throw new Exception("have not init!!!! try Init()");
            await Task.Run(() =>
            {
                MainFlow();
            });
        }
        /// <summary>
        /// 设置参数并且异步执行这个FunctionFlow
        /// </summary>
        /// <param name="args"></param>
        public async void RunAsync(TArgs args)
        {
            Initialization(args);
            await Task.Run(() =>
            {
                MainFlow();
            });
        }
        /// <summary>
        /// 设置参数同步执行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public TResult Run(TArgs args)
        {
            Initialization(args);
            return Run();
        }
        /// <summary>
        /// 同步执行
        /// </summary>
        /// <returns></returns>
        public TResult Run()
        {

            _isSync = true;
            Logger.Info(this,"Run sync....");
            if (_args == null) throw new Exception("have not init!!!! try Init()");
            MainFlow();
            return _resultTmp;
        }
        /// <summary>
        /// 强制停止这个FunctionFlow
        /// </summary>
        public virtual void ForceStop()
        {
            Logger.Warn(this,"Try to force Stop");
            if (_pid == null) return;
            SystemHelper.KillProcessAndChildrens((int)_pid);
            isForceStoped = true;
            Logger.Info(this,"Force stoped...");
            return;
        }
        /// <summary>
        /// 获取这个FuntionFlow的停止器
        /// </summary>
        /// <returns></returns>
        public Stoper GetStoper()
        {
            return new Stoper(this);
        }
        /// <summary>
        /// 析构将会调用ForceStop
        /// </summary>
        public void Dispose()
        {
            ForceStop();
        }
    }
}
