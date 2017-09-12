/*
 功能模块父类
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.AdbEnc;
using AutumnBox.Basic.Functions.Event;
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
        //PUBLIC
        public event StartEventHandler Start;
        public event FinishEventHandler Finish;//完成操作时的事件
        //PROTECTED
        protected Thread MainThread { get; set; }//异步执行的主要线程
        //INTERNAL
        internal Adb adb;//adb执行器
        internal Fastboot fastboot;//fastboot执行器
        internal static FunctionRequiredDeviceStatus RequiredDeviceStatus =
            FunctionRequiredDeviceStatus.All;//功能执行所需的手机状态,这将会决定功能模块的初始化,建议子类根据需要覆写
        internal virtual string DeviceID { get; set; }//功能执行时的设备ID
        internal bool IsFinishEventBound
        {
            get
            {
                return Finish == null ? false : true;
            }
        }//判断完成事件是否被绑定

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="needStatus">功能模块需要的手机状态</param>
        internal FunctionModule(FunctionRequiredDeviceStatus needStatus = FunctionRequiredDeviceStatus.All) : base()
        {
            switch (needStatus)
            {
                case FunctionRequiredDeviceStatus.Fastboot:
                    fastboot = new Fastboot();
                    break;
                case FunctionRequiredDeviceStatus.All:
                    adb = new Adb();
                    fastboot = new Fastboot();
                    break;
                default:
                    adb = new Adb();
                    break;
            }
            TAG = this.GetType().Name;
        }
        /// <summary>
        /// 开始执行函数
        /// </summary>
        /// <param name="delayTime">延迟执行的时间,如果不写则延迟0秒</param>
        /// <returns></returns>
        internal virtual RunningManager Run(int delayTime = 0)
        {
            OnStart(this, new StartEventArgs());
            MainThread = new Thread(() => { Thread.Sleep(delayTime); MainMethod(); });
            MainThread.Name = TAG + " MainMethod";
            var rm = new RunningManager(this);
            MainThread.Start();
            LogD($"Run MainMethod DelayTime{delayTime}(ms)");
            return rm;
        }

        /// <summary>
        /// 准备执行核心功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="a"></param>
        protected virtual void OnStart(object sender, StartEventArgs a)
        {
            Logger.D(TAG, "Start");
            Start?.Invoke(sender, a);
        }
        /// <summary>
        /// 当核心代码执行完成时,必须调用此方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="o"></param>
        protected virtual void OnFinish(object sender, FinishEventArgs a)
        {
            Logger.D(TAG, "Finish");
            Finish?.Invoke(sender, a);
        }

        /// <summary>
        /// 模块的核心代码,需要子类进行实现
        /// </summary>
        protected abstract void MainMethod();
    }
}
