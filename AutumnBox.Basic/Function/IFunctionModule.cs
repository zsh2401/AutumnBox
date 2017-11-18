using AutumnBox.Basic.Executer;
using AutumnBox.Basic.Function.Args;
using AutumnBox.Basic.Function.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Function
{
    /// <summary>
    /// 功能模块接口
    /// </summary>
    public interface IFunctionModule
    {
        event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 模块开始运行时发生
        /// </summary>
        event StartupEventHandler Startup;
        /// <summary>
        /// 模块完成运行时发生
        /// </summary>
        event FinishedEventHandler Finished;
        /// <summary>
        /// 模块中的核心进程启动时发生
        /// </summary>
        event ProcessStartedEventHandler CoreProcessStarted;
        /// <summary>
        /// 模块的状态
        /// </summary>
        ModuleStatus Status { get; }
        /// <summary>
        /// 是否已经注册了Finished事件
        /// </summary>
        bool IsFinishedEventRegistered { get; }
        /// <summary>
        /// 模块的参数
        /// </summary>
        ModuleArgs Args { set; }
        /// <summary>
        /// 开始运行
        /// </summary>
        void Run();
        /// <summary>
        /// 强制停止
        /// </summary>
        void ForceStop();
    }
}
