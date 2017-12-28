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
#define SHOW_COMMAND
namespace AutumnBox.Basic.Executer
{
    using AutumnBox.Support.CstmDebug;
    using System;
    using System.Diagnostics;
    public delegate void OutputReceivedEventHandler(object sender, OutputReceivedEventArgs e);
    public delegate void ProcessStartedEventHandler(object sender, ProcessStartedEventArgs e);
    public class OutputReceivedEventArgs : EventArgs
    {
        public bool IsError { get; private set; }
        public string Text { get; private set; }
        public DataReceivedEventArgs SourceArgs { get; private set; }
        public OutputReceivedEventArgs(string text, DataReceivedEventArgs source, bool isError = false)
        {
            Text = text;
            IsError = isError;
            SourceArgs = source;
        }
    }
    public class ProcessStartedEventArgs : EventArgs
    {
        public int Pid { get; set; }
    }
    public sealed class ABProcess : Process, IOutSender
    {
        public bool BlockNullOutput { get; set; } = true;
        public event OutputReceivedEventHandler OutputReceived;
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
            OutputDataReceived += (s, e) =>
            {
                if (BlockNullOutput && (e.Data == null || e.Data == String.Empty)) return;
                OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, e, false));
                _tempOut.OutAdd(e.Data);
            };
            ErrorDataReceived += (s, e) =>
            {
                if (BlockNullOutput && (e.Data == null || e.Data == String.Empty)) return;
                OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, e, true));
                _tempOut.ErrorAdd(e.Data);
            };
        }
        public void BeginRead()
        {
            try
            {
                BeginOutputReadLine();
                BeginErrorReadLine();
            }
            catch (Exception e) { Logger.T("Begin Out failed", e); }
        }
        public void CancelRead()
        {
            try
            {
                base.CancelOutputRead();
                base.CancelErrorRead();
                Close();
            }
            catch (Exception e) { Logger.D("等待退出或关闭流失败", e); }
        }
        public OutputData RunToExited(string fileName, string args)
        {
#if SHOW_COMMAND
            Logger.D($"{fileName} {args}");
#endif
            _tempOut = new OutputData();
            StartInfo.FileName = fileName;
            StartInfo.Arguments = args;
            Start();
            ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = this.Id });
            BeginRead();
            WaitForExit();
            Logger.D(ExitCode.ToString());
            CancelRead();
            return _tempOut;
        }
    }
}
