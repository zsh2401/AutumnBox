using AutumnBox.Basic.Device;
using System;
using System.Diagnostics;

namespace AutumnBox.Basic.Calling.Adb
{
    /// <summary>
    /// 相比ShellCommand来说，能够更加实时获取输出的ShellCommand
    /// </summary>
    [Obsolete("极度不可靠,请使用AndroidShell代替")]
    public class RealtimeShellCommand : AdbCommand
    {
        private readonly string shellCommand;
        /// <summary>
        /// 构造RealtimeShellCommand实例
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="shellCommand">具体的shell命令</param>
        public RealtimeShellCommand(IDevice device,string shellCommand) : base(device,"shell")
        {
            if (device == null)
            {
                throw new ArgumentNullException(nameof(device));
            }

            this.shellCommand = shellCommand ?? throw new ArgumentNullException(nameof(shellCommand));
        }
        /// <summary>
        /// 覆写执行前函数，进行一些设置
        /// </summary>
        /// <param name="procStartInfo"></param>
        protected override void BeforeProcessStart(ProcessStartInfo procStartInfo)
        {
            base.BeforeProcessStart(procStartInfo);
            procStartInfo.RedirectStandardInput = true;
        }
        /// <summary>
        /// 覆写进程开始后的回调函数
        /// </summary>
        /// <param name="proc"></param>
        protected override void OnProcessStarted(Process proc)
        {
            base.OnProcessStarted(proc);
            proc.StandardInput.WriteLine(shellCommand);
            proc.StandardInput.WriteLine("exit");
        }
        /// <summary>
        /// 覆写以处理输出数据
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isErr"></param>
        protected override void RaiseOutputReceived(DataReceivedEventArgs e, bool isErr)
        {
            base.RaiseOutputReceived(e, isErr);
        }
    }
}
