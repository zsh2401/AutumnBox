/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/26 2:15:44 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }
    }
}
