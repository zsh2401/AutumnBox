//#define SHOW_OUTPUT
//#define SHOW_COMMAND
namespace AutumnBox.Basic.Executer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Util;
    public sealed partial class CommandExecuter : BaseObject, IDisposable
    {
        /// <summary>
        /// 执行器主进程开始时发生,可通过该事件获取进程PID
        /// </summary>
        public event ProcessStartedEventHandler ProcessStarted
        {
            add { ABProcess.ProcessStarted += value; }
            remove { ABProcess.ProcessStarted -= value; }
        }
        /// <summary>
        /// 接收到重定向输出时发生
        /// </summary>
        public event DataReceivedEventHandler OutputDataReceived
        {
            add { ABProcess.OutputDataReceived += value; }
            remove { ABProcess.OutputDataReceived -= value; }
        }
        /// <summary>
        /// 接收到重定向错误时发生
        /// </summary>
        public event DataReceivedEventHandler ErrorDataReceived
        {
            add { ABProcess.ErrorDataReceived += value; }
            remove { ABProcess.ErrorDataReceived -= value; }
        }
        /// <summary>
        /// 执行器的底层进程
        /// </summary>
        private ABProcess ABProcess = new ABProcess();

        /// <summary>
        /// 启动adb服务
        /// </summary>
        public static void Start()
        {
            new CommandExecuter().AdbExecute("start-server");
        }
        /// <summary>
        /// 关闭adb服务
        /// </summary>
        public static void Kill()
        {
            new CommandExecuter().AdbExecute("kill-server");
        }
        /// <summary>
        /// 重启adb服务
        /// </summary>
        public static void Restart()
        {
            Kill();
            Start();
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void Dispose()
        {
            ABProcess.Dispose();
            Kill();
        }
    }
}
