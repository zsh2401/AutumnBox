/* =============================================================================*\
*
* Filename: ABProcess.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 00:54:56(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Executer
{
    using AutumnBox.Basic.Util;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    internal sealed class ABProcess : Process
    {
        public event ProcessStartedEventHandler ProcessStarted;
        private OutputData _tempOut = new OutputData();
        public ABProcess() : base()
        {
            StartInfo = new ProcessStartInfo()
            {
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
            };
            this.OutputDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    if (DebugInfo.SHOW_OUTPUT)
                        Logger.D(this.GetType().Name, e.Data);
                    _tempOut.OutAdd(e.Data);
                }

            };
            this.ErrorDataReceived += (s, e) =>
            {
                if (e.Data != null)
                {
                    if (DebugInfo.SHOW_OUTPUT)
                        Logger.D(this.GetType().Name, e.Data);
                    _tempOut.ErrorAdd(e.Data);
                }

            };
        }
        private void BeginRead()
        {
            try
            {
                BeginOutputReadLine();
                BeginErrorReadLine();
            }
            catch (Exception e) { Logger.E(this.GetType().Name, "Begin Out failed", e); }
        }
        private void CancelRead()
        {
            try
            {
                base.CancelOutputRead();
                base.CancelErrorRead();
                Close();
            }
            catch (Exception e) { Logger.E(this.GetType().Name, "等待退出或关闭流失败", e); }
        }
        public OutputData RunToExited(string fileName, string args)
        {
            if (DebugInfo.SHOW_COMMAND)
                Logger.D(this.GetType().Name,$"{fileName} {args}");
            _tempOut.Clear();
            StartInfo.FileName = fileName;
            StartInfo.Arguments = args;
            base.Start();
            BeginRead();
            ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { PID = Id });
            WaitForExit();
            int exitCode  = ExitCode;
            CancelRead();
            return _tempOut;
        }
    }
}
