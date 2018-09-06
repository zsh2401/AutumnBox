/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 22:05:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;
using System.Diagnostics;
using System.Text;

namespace AutumnBox.Basic.Executer
{
    /// <summary>
    /// 轻量级的AndroidShell
    /// </summary>
    public sealed class AndroidShellV2 : IOutputable
    {
        /// <summary>
        /// Linux用户
        /// </summary>
        public enum LinuxUser
        {
            /// <summary>
            /// 普通用户
            /// </summary>
            Normal,
            /// <summary>
            /// 超级用户(su/root)
            /// </summary>
            Su
        }
        /// <summary>
        /// 接收到输出时发生
        /// </summary>
        public event OutputReceivedEventHandler OutputReceived;
        /// <summary>
        /// 主进程开始时发生
        /// </summary>
        public event ProcessStartedEventHandler ProcessStarted;
       
        private readonly ProcessStartInfo pStartInfo;
        private readonly DeviceSerialNumber device;
        private readonly object exeLock = new object();
        private AdvanceOutputBuilder outputBuilder;

        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="device"></param>
        public AndroidShellV2(DeviceSerialNumber device)
        {
            this.device = device;
            outputBuilder = new AdvanceOutputBuilder();
            pStartInfo = new ProcessStartInfo()
            {
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = AdbConstants.FullAdbFileName,
            };
        }

        /// <summary>
        /// 检测是否有root权限
        /// </summary>
        /// <returns></returns>
        public bool HaveSu()
        {
            return Execute("ls", LinuxUser.Su,false).GetExitCode() == 0;
        }

        /// <summary>
        /// 执行一段命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="user">用户类型</param>
        /// <param name="suCheck">当root执行时,检测su是否存在并根据情况抛出异常</param>
        /// <returns></returns>
        public AdvanceOutput Execute(string command, LinuxUser user = LinuxUser.Normal,bool suCheck=true)
        {
            lock (exeLock)
            {
                outputBuilder.Clear();
                if (user == LinuxUser.Su && suCheck && !HaveSu())
                {
                    throw new DeviceHasNoSuException();
                }
                string shellCommand = user == LinuxUser.Normal ? command : $"su -c \'{command}\'";
                string fullCommand = $"{device.ToFullSerial()} shell \"{shellCommand} ; exit $?\"";
                pStartInfo.Arguments = fullCommand;
                using (var process = new Process())
                {
                    process.StartInfo = pStartInfo;
                    process.OutputDataReceived += (s, e) => { OnOutputReceived(e, false); };
                    process.ErrorDataReceived += (s, e) => { OnOutputReceived(e, true); };
                    process.Start();
                    process.BeginOutputReadLine();
                    process.BeginErrorReadLine();
                    ProcessStarted?.Invoke(this, new ProcessStartedEventArgs(process.Id));
                    process.WaitForExit();
                    process.CancelErrorRead();
                    process.CancelOutputRead();
                    outputBuilder.ExitCode = process.ExitCode;
                }
                return outputBuilder.Result;
            }
        }

        private void OnOutputReceived(DataReceivedEventArgs srcArgs, bool isErr = false)
        {
            if (isErr)
            {
                outputBuilder.AppendError(srcArgs.Data);
            }
            else
            {
                outputBuilder.AppendOut(srcArgs.Data);
            }
            OutputReceived?.Invoke(this, new OutputReceivedEventArgs(srcArgs, isErr));
        }
    }
}
