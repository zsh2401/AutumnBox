/*
 功能模块父类
 @zsh2401
 2017/9/8
 */
using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Functions.Event;
using AutumnBox.Basic.Functions.Interface;
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
        //Internal
        internal event StartEventHandler Started;//当功能模块开始执行时发生
        internal event FinishEventHandler Finished;//完成操作时的事件
        //PROTECTED
        protected Thread MainThread { get; set; }//异步执行的主要线程
        /// <summary>
        /// 向已绑定的设备执行adb命令
        /// </summary>
        protected Func<string, OutputData> ae { get; private set; }
        /// <summary>
        /// 向已绑定的设备执行fastboot命令
        /// </summary>
        protected Func<string, OutputData> fe { get; private set; }
        /// <summary>
        /// 执行器
        /// </summary>
        protected internal  CommandExecuter executer = new CommandExecuter();
        /// <summary>
        /// 功能模块执行时指定的设备id
        /// </summary>
        protected internal string DeviceID { get; internal set; }//功能执行时的设备ID
        protected internal FunctionModule()
        {
            ae = (command) => {
                executer.ExecuteWithDevice(DeviceID, command, out OutputData o, ExeType.Adb);
                return o;
            };
            fe = (command) => {
                executer.ExecuteWithDevice(DeviceID, command, out OutputData o, ExeType.Fastboot);
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
            MainThread = new Thread(() => { Thread.Sleep(delayTime); _Run(); });
            MainThread.Name = TAG + " MainMethod";
            MainThread.Start();
            LogD($"Run MainMethod DelayTime {delayTime} (ms)");
        }
        /// <summary>
        /// 运行过程
        /// </summary>
        private void _Run()
        {
            OnStart(this, new StartEventArgs());
            var o = MainMethod();
            OnFinish(this, new FinishEventArgs { OutputData = o, IsFinish = true });
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
