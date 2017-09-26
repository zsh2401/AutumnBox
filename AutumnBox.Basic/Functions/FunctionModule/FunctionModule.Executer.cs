namespace AutumnBox.Basic.Functions
{
    using AutumnBox.Basic.Executer;
    using AutumnBox.Basic.Util;
    using System;
    using System.Diagnostics;
//    partial class FunctionModule
//    {
//        private Process CmdProcess;
//        private OutputData _tempOut = new OutputData();
//        private void InitExecuter() {
//             CmdProcess = new Process()
//             {
//                 StartInfo = new ProcessStartInfo()
//                 {
//                     RedirectStandardError = true,
//                     RedirectStandardOutput = true,
//                     RedirectStandardInput = true,
//                     UseShellExecute = false,
//                     CreateNoWindow = true
//                 },
//             };
//            CmdProcess.OutputDataReceived += (s, e) => {
//#if SHOW_OUT
//                 LogD(e.Data);
//#endif
//                _tempOut.OutAdd(e.Data);
//                OnOutReceived(e); };
//            CmdProcess.ErrorDataReceived += (s, e) => {
//#if SHOW_OUT
//                LogD(e.Data);
//#endif
//                _tempOut.ErrorAdd(e.Data);
//                OnErrorReceived(e); };
//        }
//        private OutputData BasicExecute(ExecuteType exeType,string fullCommand)
//        {
//            CmdProcess.StartInfo.FileName =
//                (exeType == ExecuteType.Adb) ? Paths.ADB_FILENAME:Paths.FASTBOOT_FILENAME;
//            _tempOut.Clear();
//#if SHOW_COMMAND
//            LogD($"Execute Command {fullCommand}");
//#endif
//            CmdProcess.StartInfo.Arguments = " " + fullCommand;
//            CmdProcess.Start();
//            OnProcessStarted(new ProcessStartedEventArgs() { PID = CmdProcess.Id });
//            /*开始读取进程的流输出*/
//            try
//            {
//                CmdProcess.BeginOutputReadLine();
//                CmdProcess.BeginErrorReadLine();
//            }
//            catch (Exception e) { LogE("Begin Out failed", e); }
//            /*等待进程结束后关闭流*/
//            try
//            {
//                CmdProcess.WaitForExit();
//                CmdProcess.CancelOutputRead();
//                CmdProcess.CancelErrorRead();
//                CmdProcess.Close();
//            }catch (Exception e) { LogE("等待退出或关闭流失败", e); }
//            return tempOut;
//        }
//        protected OutputData FastbootExecute(Command cmd)
//        {
//            string command = cmd.IsDesignatedDevice == true
//                ? $" -s {cmd.DeviceID} {cmd.SpecificCommand}": cmd.SpecificCommand;
//            return BasicExecute(cmd.ExecuteType,command);
//        }
//        protected OutputData AdbExecute(Command cmd)
//        {

//        }
//    }
}
