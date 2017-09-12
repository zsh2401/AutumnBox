using AutumnBox.Basic.Functions.ExecutedResultHandler;
using AutumnBox.Basic.Util;
using System.Diagnostics;

namespace AutumnBox.Basic.Functions
{
    public sealed class RunningManager
    {
        //PUBLIC
        public bool FunctionIsFinish { get { return _funcIsFinish; } }
        private bool _funcIsFinish = false;
        public FunctionModule Fm { get; private set; }
        public bool FuncCanStop
        {
            get
            {
                return (Fm as IFunctionCanStop) == null ? false : true;
            }
        }
        //public event DataReceivedEventHandler OutputDataReceived
        //{
        //    add
        //    {
        //        if ((Fm as ICanGetRealTimeOut) != null)
        //            (Fm as ICanGetRealTimeOut).OutputDataReceived += value;
        //    }
        //    remove
        //    {
        //        if ((Fm as ICanGetRealTimeOut) != null)
        //            (Fm as ICanGetRealTimeOut).OutputDataReceived -= value;
        //    }
        //}
        //public event DataReceivedEventHandler ErrorDataReceived
        //{
        //    add
        //    {
        //        if ((Fm as ICanGetRealTimeOut) != null)
        //            (Fm as ICanGetRealTimeOut).ErrorDataReceived += value;

        //    }
        //    remove
        //    {
        //        if ((Fm as ICanGetRealTimeOut) != null)
        //            (Fm as ICanGetRealTimeOut).ErrorDataReceived -= value;
        //    }
        //}
        public string FunctionName { get { return Fm.GetType().Name; } }
        internal RunningManager(FunctionModule fm)
        {
            this.Fm = fm;
            Fm.Finish += (s, e) => { _funcIsFinish = true; };
        }
        /// <summary>
        /// 停止执行管理的正在运行的功能,但需要该功能模块实现IFunctionCanStop
        /// </summary>
        public void FuncStop()
        {
            if ((Fm as IFunctionCanStop) != null)
            {
                int mainPid = (Fm as IFunctionCanStop).CmdProcessPID;
                Tools.KillProcessAndChildrens(mainPid);
            }
            else
            {
                throw new FuncNotSupportStopException();
            }
        }
    }
}
