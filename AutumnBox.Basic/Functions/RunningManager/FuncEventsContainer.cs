namespace AutumnBox.Basic.Functions.RunningManager
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Interface;
    using AutumnBox.Basic.Util;
    using System.Diagnostics;
    /// <summary>
    /// 整合了一些功能模块的事件
    /// </summary>
    public class FuncEventsContainer : BaseObject
    {
        /****************************PUBLIC************************/
        bool IsSetOutReceiver = false;
        public IOutReceiver OutReceiver { set {
                if (!IsSetOutReceiver) {
                    OutputReceived += value.OutReceived;
                    ErrorReceived += value.ErrorReceived;
                    IsSetOutReceiver = true;
                }
            } }
        public event DataReceivedEventHandler OutputReceived
        {
            add
            {
                AddCheck();
                Fm.OutReceived += value;
            }
            remove
            {
                Fm.OutReceived -= value;
            }
        }
        public event DataReceivedEventHandler ErrorReceived
        {
            add
            {
                AddCheck();
                Fm.ErrorReceived += value;
            }
            remove
            {
                Fm.ErrorReceived -= value;
            }
        }
        public event StartedEventHandler Started
        {
            add { AddCheck(); Fm.Started += value; }
            remove { Fm.Started -= value; }
        }
        public event FinishedEventHandler Finished
        {
            add { AddCheck(); Fm.Finished += value; }
            remove { Fm.Finished -= value; }
        }
        public event ProcessStartedEventHandler ProcessStarted
        {
            add { AddCheck(); Fm.ProcessStarted += value; }
            remove { Fm.ProcessStarted -= value; }
        }
        /****************************PUBLIC***********************/

        private FunctionModule Fm { get; set; }
        private RunningManager Rm { get; set; }
        private void AddCheck()
        {
            //if (Rm.Status != RunningManagerStatus.Loaded && Rm.Status != RunningManagerStatus.Loading)
            //{
            //    throw new EventAddException("Please add eventhandler on Function not started");
            //}
        }
        internal FuncEventsContainer(RunningManager rm)
        {
            this.Fm = rm.Fm;
        }
    }
}
