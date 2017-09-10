using AutumnBox.Basic.Util;
namespace AutumnBox.Basic.Functions
{
    public sealed class RunningManager
    {
        //PUBLIC
        public bool FunctionIsFinish = false;
        public FunctionModule Fm;
        public bool FuncCanStop { get; private set; }
        public OutReceiver OutRecevier { get; private set; }
        public string FunctionName { get { return Fm.GetType().Name; } }
        internal RunningManager(FunctionModule fm)
        {
            this.Fm = fm;
            Fm.Finish += (s, e) => { FunctionIsFinish = true; };
            if ((Fm as ICanGetRealTimeOut) != null)
            {
                FuncCanStop = true;
                OutRecevier = new OutReceiver((Fm as ICanGetRealTimeOut));
            }
            else {
                FuncCanStop = false;
            }
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
