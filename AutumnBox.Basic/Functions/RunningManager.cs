using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Functions.Interface;
using AutumnBox.Basic.Util;
using System.Diagnostics;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 运行时托管器
    /// </summary>
    public sealed class RunningManager
    {
        //PUBLIC
        public bool FunctionIsFinish
        {
            get { return _funcIsFinish; }
        }
        public string FunctionName { get { return Fm.GetType().Name; } }
        #region 事件
        public event DataReceivedEventHandler OutputReceived
        {
            add
            {
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
                Fm.executer.ErrorDataReceived += value;
            }
            remove
            {
                Fm.executer.ErrorDataReceived -= value;
            }
        }
        public event StartEventHandler FuncStarted
        {
            add { Fm.Started += value; }
            remove { Fm.Started -= value; }
        }
        public event FinishEventHandler FuncFinished
        {
            add { Fm.Finished += value; }
            remove { Fm.Finished -= value; }
        }
        public event ExecuteStartHandler ExecuterStared
        {
            add { Fm.executer.ExecuteStarted += value; }
            remove { Fm.executer.ExecuteStarted -= value; }
        }
        public event RunningManagerFinishHandler Finished;
        #endregion
        //PRIVATE
        private FunctionModule Fm { get; set; }
        private int _pid;
        private bool _funcIsFinish = false;
        /// <summary>
        /// 构造!
        /// </summary>
        /// <param name="fm"></param>
        internal RunningManager(FunctionModule fm)
        {
            this.Fm = fm;
            //绑定好事件,在进程开始时获取PID用于结束进程
            
            Fm.executer.ExecuteStarted += (s_, e_) => { _pid = e_.PID; };
        }
        /// <summary>
        /// 开始执行托管的功能模块
        /// </summary>
        public void FuncStart()
        {
            if (!Fm.IsFinishEventBound) throw new EventNotBoundException();
            Fm.Finished += (s, e) => { _funcIsFinish = true; Finished?.Invoke(this, new RMFinishEventArgs()); };
            if (FunctionIsFinish) return;
            Logger.D("FuntionIsFinish?", Fm.IsFinishEventBound.ToString());
            Fm.Run();
        }
        /// <summary>
        /// 停止执行管理的正在运行的功能,但需要该功能模块实现IFunctionCanStop
        /// </summary>
        public void FuncStop()
        {
            Tools.KillProcessAndChildrens(_pid);
        }
    }
}
