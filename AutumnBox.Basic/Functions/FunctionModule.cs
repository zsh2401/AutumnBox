/*
 功能模块父类
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Util;
using System;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 各种功能模块的父类
    /// </summary>
    public abstract class FunctionModule : BaseObject, IDisposable
    {
        //PROTECTED
        protected Thread MainThread { get; set; }//异步执行的主要线程
        protected Func<string, string, OutputData> ae { get; private set; }
        protected Func<string, string, OutputData> fe { get; private set; }
        protected internal  CommandExecuter executer = new CommandExecuter();
        protected internal event StartEventHandler Started;//当功能模块开始执行时发生
        protected internal event FinishEventHandler Finished;//完成操作时的事件
        protected internal string DeviceID { get; set; }//功能执行时的设备ID

        //Set by DeviceLink Object
        [Obsolete]
        internal DeviceLink.LinkType LinkType { get; set; }
        protected internal FunctionModule()
        {
            ae = (id,command) => {
                executer.ExecuteWithDevice(id, command, out OutputData o, ExeType.Adb);
                return o;
            };
            fe = (id, command) => {
                executer.ExecuteWithDevice(id, command, out OutputData o, ExeType.Fastboot);
                return o;
            };
            TAG = GetType().Name;
        }
        /// <summary>
        /// 判断完成事件是否被绑定
        /// </summary>
        protected internal bool IsFinishEventBound
        {
            get
            {
                return Finished != null ? true : false;
            }
        }
        /// <summary>
        /// 开始执行函数
        /// </summary>
        /// <param name="delayTime">延迟执行的时间,如果不写则立刻开始</param>
        /// <returns></returns>
        internal void Run(int delayTime = 0)
        {
            MainThread = new Thread(() => { Thread.Sleep(delayTime); CoreRun(); });
            MainThread.Name = TAG + " MainMethod";
            MainThread.Start();
            LogD($"Run MainMethod DelayTime {delayTime} (ms)");
        }
        /// <summary>
        /// 运行过程
        /// </summary>
        private void CoreRun()
        {
            OnStart(this, new StartEventArgs());
            var o = MainMethod();
            OnFinish(this, new FinishEventArgs { OutErrorData = o, IsFinish = true });
        }

        /// <summary>
        /// 准备执行核心功能时,将调用此方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a"></param>
        protected virtual void OnStart(object sender, StartEventArgs a)
        {
            Logger.D(TAG, "Start");
            Started?.Invoke(sender, a);
        }
        /// <summary>
        /// 当核心代码执行完成时,将调用此方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        protected virtual void OnFinish(object sender, FinishEventArgs a)
        {
            Logger.D(TAG, "Finish");
            Finished?.Invoke(sender, a);
        }

        /// <summary>
        /// 模块的核心代码,强制要求子类进行实现
        /// </summary>
        protected abstract OutputData MainMethod();

        public void Dispose()
        {
            executer.Dispose();
        }
    }
}
