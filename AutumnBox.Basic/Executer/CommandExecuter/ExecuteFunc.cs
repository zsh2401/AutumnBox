/*
 命令执行器的主要执行命令函数
 */
namespace AutumnBox.Basic.Executer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    partial class CommandExecuter {
        /// <summary>
        /// 基础的命令执行器
        /// </summary>
        /// <param name="command"></param>
        /// <param name="o"></param>
        public OutputData BasicExecute(ExeType type,string command)
        {
            tempOut.Clear();
            NowExeType = type;
#if SHOW_COMMAND
            LogD($"Execute Command {command}");
#endif
            MainProcess.StartInfo.Arguments = " " + command;
            MainProcess.Start();
            ProcessStarted?.Invoke(this,new ProcessStartEventArgs() { PID = MainProcess.Id});
            try
            {
                MainProcess.BeginOutputReadLine();
                MainProcess.BeginErrorReadLine();
            }
            catch (Exception e) { LogE("Begin Out failed", e); }
            try
            {
                MainProcess.WaitForExit();
                MainProcess.CancelOutputRead();
                MainProcess.CancelErrorRead();
                MainProcess.Close();
            }
            catch (Exception e) { LogE("等待退出或关闭流失败", e); }
            return tempOut;
        }
        /// <summary>
        /// 执行adb命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData AdbExecute(string command) {
            return BasicExecute(ExeType.Adb, command);
        }
        /// <summary>
        /// 向指定设备执行adb命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData AdbExecute(string devId, string command) {
            return AdbExecute($" -s {devId} {command}");
        }
        /// <summary>
        /// 执行fastboot命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData FastbootExecute(string command) {
            return BasicExecute(ExeType.Fastboot, command);
        }
        /// <summary>
        /// 向指定设备执行fastboot命令
        /// </summary>
        /// <param name="devId"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public OutputData FastbootExecute(string devId,string command) {
            return FastbootExecute($" -s {devId} {command}");
        }
    }
}
