/*************************************************
** auth： zsh2401@163.com
** date:  2018/2/21 22:05:01 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Executer
{
    public sealed class AndroidShellV2 : IOutSender
    {
        public enum LinuxUser
        {
            Normal,
            Su
        }
        public event OutputReceivedEventHandler OutputReceived;
        public event ProcessStartedEventHandler ProcessStarted;
        private readonly ProcessStartInfo pStartInfo;
        private readonly DeviceSerial device;
        private readonly object exeLock = new object();
        public AndroidShellV2(DeviceSerial device)
        {
            this.device = device;
            outputBuilder = new AdvanceOutputBuilder();
            pStartInfo = new ProcessStartInfo()
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                FileName = AdbConstants.FullAdbFileName,
            };
        }
        private AdvanceOutputBuilder outputBuilder;
        public bool HaveSu()
        {
            return Execute("ls", LinuxUser.Su,false).ExitCode == 0;
        }
        public AdvanceOutput Execute(string command, LinuxUser user = LinuxUser.Normal,bool suCheck=true)
        {
            lock (exeLock)
            {
                outputBuilder.Clear();
                if (user == LinuxUser.Su && suCheck && !HaveSu())
                {
                    throw new DeviceHaveNoRootException(device);
                }
                string shellCommand = user == LinuxUser.Normal ? command : $"su -c \'{command}\'";
                string fullCommand = $"{device.ToFullSerial()} shell \"{shellCommand} ; exit $?\"";
                pStartInfo.Arguments = fullCommand;
                Console.WriteLine(fullCommand);
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
