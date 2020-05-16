/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 2:15:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.ManagedAdb.CommandDriven;
using AutumnBox.Logging;
using System;
using System.Diagnostics;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// Process拓展
    /// </summary>
    public static class ProcessExtensions
    {
        /// <summary>
        /// 判断是否在运行
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 使用Windows taskkill命令进行结束
        /// </summary>
        /// <param name="proc"></param>
        /// <exception cref="PlatformNotSupportedException">此命令仅支持WIN32平台,在其它平台调用将抛出此异常</exception>
        public static void TaskKill(this Process proc)
        {
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                var logger = LoggerFactory.Auto(nameof(ProcessExtensions));
                var cmd = new CommandProcedure()
                {
                    FileName = "taskkill",
                    Arguments = $"/F /PID { proc.Id }"
                };
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }
    }
}
