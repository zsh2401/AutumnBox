/*
 功能模块父类
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Functions.FunctionArgs;
using AutumnBox.Basic.Util;
using System;
using System.Threading;

namespace AutumnBox.Basic.Functions
{
    /// <summary>
    /// 各种功能模块的父类
    /// </summary>
    public abstract class FunctionModule : BaseObject
    {
        //PROTECTED
        protected Thread MainThread { get; set; }//异步执行的主要线程
        //INTERNAL
        internal IAdbCommandExecuter MainExecuter { get;set; }
        internal event StartEventHandler Started;//当功能模块开始执行时发生
        internal event FinishEventHandler Finished;//完成操作时的事件
        internal virtual string DeviceID { get; set; }//功能执行时的设备ID
        internal bool IsFinishEventBound
        {
            get
            {
                return Finished != null ? true : false;
            }
        }//判断完成事件是否被绑定
        //[Obsolete]
        //internal FunctionModule(FunctionRequiredDeviceStatus needStatus = FunctionRequiredDeviceStatus.All) : base()
        //{
        //    switch (needStatus)
        //    {
        //        case FunctionRequiredDeviceStatus.Fastboot:
        //            //fastboot = new Fastboot();
        //            MainExecuter = new Fastboot();
        //            break;
        //        case FunctionRequiredDeviceStatus.All:
        //            adb = new Adb();
        //            fastboot = new Fastboot();
        //            break;
        //        default:
        //            MainExecuter = new Adb();
        //            break;
        //    }
        //    TAG = this.GetType().Name;
        //}
        internal FunctionModule(ExecuterInitType type = ExecuterInitType.Adb)
        {
            switch (type)
            {
                case ExecuterInitType.Adb:
                    MainExecuter = new Adb();
                    break;
                case ExecuterInitType.Fastboot:
                    MainExecuter = new Fastboot();
                    break;
                case ExecuterInitType.None:
                    break;
            }
        }
        /// <summary>
        /// 开始执行函数
        /// </summary>
        /// <param name="delayTime">延迟执行的时间,如果不写则延迟0秒</param>
        /// <returns></returns>
        internal virtual void Run(int delayTime = 0)
        {
            if (MainExecuter == null) throw new Exception();
            OnStart(this, new StartEventArgs());
            MainThread = new Thread(() => { Thread.Sleep(delayTime); MainMethod(); });
            MainThread.Name = TAG + " MainMethod";
            MainThread.Start();
            LogD($"Run MainMethod DelayTime {delayTime} (ms)");
        }

        /// <summary>
        /// 准备执行核心功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a"></param>
        protected virtual void OnStart(object sender, StartEventArgs a)
        {
            Logger.D(TAG, "Start");
            Started?.Invoke(sender, a);
        }
        /// <summary>
        /// 当核心代码执行完成时,必须调用此方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        protected virtual void OnFinish(object sender, FinishEventArgs a)
        {
            Logger.D(TAG, "Finish");
            Finished?.Invoke(sender, a);
        }

        /// <summary>
        /// 模块的核心代码,需要子类进行实现
        /// </summary>
        protected abstract void MainMethod();
    }
}
