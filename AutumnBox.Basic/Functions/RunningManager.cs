namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Event;
    using AutumnBox.Basic.Functions.Interface;
    using AutumnBox.Basic.Util;
    using System;
    using System.Diagnostics;
    /// <summary>
    /// 功能模块运行时托管器,一个托管器仅可以托管/包装一个功能模块,并只可以执行一次
    /// </summary>
    public sealed class RunningManager
    {
        public FuncEventsContainer FuncEvents { get; private set; }//各种功能模块的事件都放在里面方便添加处理函数
        public RunningManagerStatus Status { get; private set; }//当前托管器的状态
        public FunctionModule Fm { get; private set; }//被托管的功能模块
        private int _pid;//在功能模块中的命令执行器开始时,pid将会被赋值,以用于终止执行
        /// <summary>
        /// 构造!
        /// </summary>
        /// <param name="fm"></param>
        internal RunningManager(FunctionModule fm)
        {
            this.Fm = fm;
            this.FuncEvents = FuncEventsContainer.Get(this);
            //绑定好事件,在进程开始时获取PID用于结束进程
            Fm.executer.ProcessStared += (s_, e_) => { _pid = e_.PID; };
            Status = RunningManagerStatus.Loaded;
        }
        //internal RunningManager(IExtendsFunctionMoudle fm) {

        //}
        /// <summary>
        /// 开始执行托管的功能模块
        /// </summary>
        public void FuncStart()
        {
            if (!Fm.IsFinishEventBound) throw new EventNotBoundException();
            if (this.Status != RunningManagerStatus.Loaded) throw new Exception("this Running Manager is finished,Please use new Running Manager");
            Fm.Finished += (s, e) => { Status = RunningManagerStatus.Finished; };
            Logger.D("FuntionIsFinish?", Fm.IsFinishEventBound.ToString());
            Status = RunningManagerStatus.Running;
            Fm.Run();

        }
        /// <summary>
        /// 强制停止执行管理的正在运行的功能
        /// </summary>
        public void FuncStop()
        {
            Tools.KillProcessAndChildrens(_pid);
            Status = RunningManagerStatus.Cancel;
        }
    }
    /// <summary>
    /// Nothing....
    /// </summary>
    public enum RunningManagerStatus
    {
        Loaded = 0,
        Running,
        Finished,
        Cancel,
    }
    /// <summary>
    /// 整合了一些功能模块的事件
    /// </summary>
    public struct FuncEventsContainer
    {
        private FunctionModule Fm { get; set; }
        private RunningManager Rm { get; set; }
        private void AddCheck()
        {
            if (Rm.Status != RunningManagerStatus.Loaded)
            {
                throw new EventAddException("Please add eventhandler on Function not started");
            }
        }
        public event DataReceivedEventHandler OutputReceived
        {
            add
            {
                AddCheck();
                Fm.executer.OutputDataReceived += value;
            }
            remove
            {
                Fm.executer.OutputDataReceived -= value;
            }
        }
        public event DataReceivedEventHandler ErrorReceived
        {
            add
            {
                AddCheck();
                Fm.executer.ErrorDataReceived += value;
            }
            remove
            {
                Fm.executer.ErrorDataReceived -= value;
            }
        }
        public event StartEventHandler Started
        {
            add { AddCheck(); Fm.Started += value; }
            remove { Fm.Started -= value; }
        }
        public event FinishEventHandler Finished
        {
            add { AddCheck(); Fm.Finished += value; }
            remove { Fm.Finished -= value; }
        }
        public event ExecuteStartHandler ExecuterStared
        {
            add { AddCheck(); Fm.executer.ExecuteStarted += value; }
            remove { Fm.executer.ExecuteStarted -= value; }
        }
        public event ProcessStartEventHandler ProcessStarted
        {
            add { AddCheck(); Fm.executer.ProcessStared += value; }
            remove { Fm.executer.ProcessStared -= value; }
        }
        public static FuncEventsContainer Get(RunningManager rm)
        {
            return new FuncEventsContainer { Fm = rm.Fm };
        }
    }
}
