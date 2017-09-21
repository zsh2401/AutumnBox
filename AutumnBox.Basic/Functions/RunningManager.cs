namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Event;
    using AutumnBox.Basic.Util;
    using System;
    using System.Diagnostics;
    /// <summary>
    /// 功能模块运行时托管器,一个托管器仅可以托管/包装一个功能模块,并只可以执行一次
    /// </summary>
    public sealed class RunningManager
    {
        /// <summary>
        /// 被托管的功能模块的各种事件
        /// </summary>
        public FuncEventsContainer FuncEvents { get; private set; }
        /// <summary>
        /// 当前托管器的状态
        /// </summary>
        public RunningManagerStatus Status { get; private set; }
        /// <summary>
        /// 被托管的功能模块
        /// </summary>
        public FunctionModule Fm { get; private set; }
        /// <summary>
        /// 在功能模块中的命令执行器开始时,pid将会被赋值,以用于终止执行
        /// </summary>
        private int _pid;
        /// <summary>
        /// 构造!
        /// </summary>
        /// <param name="fm"></param>
        internal RunningManager(FunctionModule fm)
        {
            Status = RunningManagerStatus.Loading;
            this.Fm = fm;
            this.FuncEvents = FuncEventsContainer.Get(this);
            //绑定好事件,在进程开始时获取PID用于结束进程
            FuncEvents.ProcessStarted += (s_, e_) => { _pid = e_.PID; };
            Status = RunningManagerStatus.Loaded;
        }
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
        /// <summary>
        /// 无需设备连接实例即可创建功能模块托管器
        /// </summary>
        /// <param name="info"></param>
        /// <param name="fm"></param>
        /// <returns></returns>
        public static RunningManager Create(DeviceSimpleInfo info, FunctionModule fm) {
            if(fm.IsFinishEventBound)throw new EventNotBoundException();
            fm.DeviceID = info.Id;
            fm.DevSimpleInfo = info;
            return new RunningManager(fm);
        }
    }
    /// <summary>
    /// Nothing....
    /// </summary>
    public enum RunningManagerStatus
    {
        Loading = -1,
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
            //if (Rm.Status != RunningManagerStatus.Loaded && Rm.Status != RunningManagerStatus.Loading)
            //{
            //    throw new EventAddException("Please add eventhandler on Function not started");
            //}
        }
        public event DataReceivedEventHandler OutputReceived
        {
            add
            {
                AddCheck();
                Fm.Executer.OutputDataReceived += value;
            }
            remove
            {
                Fm.Executer.OutputDataReceived -= value;
            }
        }
        public event DataReceivedEventHandler ErrorReceived
        {
            add
            {
                AddCheck();
                Fm.Executer.ErrorDataReceived += value;
            }
            remove
            {
                Fm.Executer.ErrorDataReceived -= value;
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
        public event ProcessStartEventHandler ProcessStarted
        {
            add { AddCheck(); Fm.Executer.ProcessStarted += value; }
            remove { Fm.Executer.ProcessStarted -= value; }
        }
        public static FuncEventsContainer Get(RunningManager rm)
        {
            return new FuncEventsContainer { Fm = rm.Fm };
        }
    }
}
