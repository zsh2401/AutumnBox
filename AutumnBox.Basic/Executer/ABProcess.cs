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
    using static Basic.DebugSettings;
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
    public sealed class ABProcess : Process
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
            OutputReceived += (s, e) =>
            {
                if (!e.IsError) _tempOut.OutAdd(e.Text);
                else _tempOut.ErrorAdd(e.Text);
            };
            OutputDataReceived += (s, e) =>
            {
                if (BlockNullOutput && e.Data == null) return;
                OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, e, false));
            };
            ErrorDataReceived += (s, e) =>
            {
                if (BlockNullOutput && e.Data == null) return;
                OutputReceived?.Invoke(this, new OutputReceivedEventArgs(e.Data, e, true));
            };
        }
        private void BeginRead()
        {
            try
            {
                BeginOutputReadLine();
                BeginErrorReadLine();
            }
            catch (Exception e) { Logger.T("Begin Out failed", e); }
        }
        private void CancelRead()
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
            //Init
            _tempOut.Clear();
            StartInfo.FileName = fileName;
            StartInfo.Arguments = args;
            //Start
            Start();
            BeginRead();
            ProcessStarted?.Invoke(this, new ProcessStartedEventArgs() { Pid = Id });
            WaitForExit();
            CancelRead();
            return _tempOut;
        }
    }
}
