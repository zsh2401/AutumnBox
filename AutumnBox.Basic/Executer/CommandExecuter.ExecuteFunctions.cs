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
        public void BasicExecute(ExeType type,string command, out OutputData o)
        {
            tempOut.Clear();
            NowExeType = type;
#if SHOW_COMMAND
            LogD($"Execute Command {command}");
#endif
            MainProcess.StartInfo.Arguments = " " + command;
            MainProcess.Start();
            ProcessStared?.Invoke(this,new ProcessStartEventArgs() { PID = MainProcess.Id});
            try
            {
                MainProcess.BeginOutputReadLine();
                MainProcess.BeginErrorReadLine();
            }
            catch (Exception e) { LogE("Begin Out failed", e); }
            ExecuteStarted?.Invoke(this, new ExecuteStartEventArgs() { PID = MainProcess.Id });
            try
            {
                MainProcess.WaitForExit();
                MainProcess.CancelOutputRead();
                MainProcess.CancelErrorRead();
                MainProcess.Close();
            }
            catch (Exception e) { LogE("等待退出或关闭流失败", e); }
            o = tempOut;
        }
        /// <summary>
        /// 向指定设备执行adb命令
        /// </summary>
        /// <param name="id">设备名</param>
        /// <param name="command">具体命令</param>
        /// <param name="o">输出数据</param>
        /// <param name="type">执行器,ADB或FASTBOOT</param>
        public void ExecuteWithDevice(string id, string command, out OutputData o, ExeType type = ExeType.Adb)
        {
            string fullCommand = $" -s {id} {command}";
            BasicExecute(type,fullCommand, out o);
        }
        /// <summary>
        /// 不特别指定设备的执行某个命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="o">输出</param>
        /// <param name="type">执行器类型</param>
        public void ExecuteWithoutDevice(string command, out OutputData o, ExeType type = ExeType.Adb)
        {
            BasicExecute(type, command, out o);
        }
        /// <summary>
        /// 向指定设备执行adb命令
        /// </summary>
        /// <param name="id">设备名</param>
        /// <param name="command">具体命令</param>
        /// <param name="o">输出数据</param>
        /// <param name="type">执行器,ADB或FASTBOOT</param>
        public OutputData ExecuteWithDevice(string id,string command,ExeType type = ExeType.Adb) {
            BasicExecute(type,$" -s {id} {command}",out OutputData o);
            return o;
        }
        /// <summary>
        /// 不特别指定设备的执行某个命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="o">输出</param>
        /// <param name="type">执行器类型</param>
        public OutputData ExecuteWithoutDevice(string command, ExeType type = ExeType.Adb) {
            BasicExecute(type, $" {command}",out OutputData o);
            return o;
        }
    }
}
