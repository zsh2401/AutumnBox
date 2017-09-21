/**
 *                             _ooOoo_
 *                            o8888888o
 *                            88" . "88
 *                            (| -_- |)
 *                            O\  =  /O
 *                         ____/`---'\____
 *                       .'  \\|     |//  `.
 *                      /  \\|||  :  |||//  \
 *                     /  _||||| -:- |||||-  \
 *                     |   | \\\  -  /// |   |
 *                     | \_|  ''\---/''  |   |
 *                     \  .-\__  `-`  ___/-. /
 *                   ___`. .'  /--.--\  `. . __
 *                ."" '<  `.___\_<|>_/___.'  >'"".
 *               | | :  `- \`.;`\ _ /`;.`/ - ` : | |
 *               \  \ `-.   \_ __\ /__ _/   .-` /  /
 *          ======`-.____`-.___\_____/___.-`____.-'======
 *                             `=---='
 *          ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
 *                     佛祖保佑        永无BUG
 *            佛曰:
 *                   写字楼里写字间，写字间里程序员；
 *                   程序人员写程序，又拿程序换酒钱。
 *                   酒醒只在网上坐，酒醉还来网下眠；
 *                   酒醉酒醒日复日，网上网下年复年。
 *                   但愿老死电脑间，不愿鞠躬老板前；
 *                   奔驰宝马贵者趣，公交自行程序员。
 *                   别人笑我忒疯癫，我笑自己命太贱；
 *                   不见满街漂亮妹，哪个归得程序员？
*/

namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Functions.Event;
    using AutumnBox.Basic.Util;
    using System;
    using System.Threading;
    /// <summary>
    /// 各种功能模块的父类
    /// </summary>
    public abstract class FunctionModule : BaseObject, IDisposable
    {
        /// <summary>
        /// 当功能模块开始执行时发生
        /// </summary>
        internal event StartEventHandler Started;
        /// <summary>
        /// 完成操作时的事件
        /// </summary>
        internal event FinishEventHandler Finished;
        /// <summary>
        /// 异步执行的主要线程
        /// </summary>
        protected Thread MainThread { get; set; }
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
        protected internal  CommandExecuter Executer = new CommandExecuter();
        /// <summary>
        /// 功能模块执行时指定的设备id
        /// </summary>
        protected internal string DeviceID {
            get { return _deviceID; }
            internal set { if (_deviceID == null) _deviceID = value;
                else throw new Exception("You can change device ID again");
            } }
        private string _deviceID;
        protected internal DeviceSimpleInfo DevSimpleInfo { get;internal set; }
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
        /// 构造!此类无法单独构造,必须被继承
        /// </summary>
        protected FunctionModule()
        {
            ae = (command) => {
                Executer.ExecuteWithDevice(DeviceID, command, out OutputData o, ExeType.Adb);
                return o;
            };
            fe = (command) => {
                Executer.ExecuteWithDevice(DeviceID, command, out OutputData o, ExeType.Fastboot);
                return o;
            };
            TAG = GetType().Name;
        }
        /// <summary>
        /// 开始执行函数,有功能模块托管器进行托管
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
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Executer.Dispose();
        }
    }
}
