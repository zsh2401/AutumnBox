/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/28 4:25:44 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Exceptions;
using AutumnBox.Basic.Util;
using System;

namespace AutumnBox.Basic.Device.Management.OS
{
    /// <summary>
    /// Shell命令帮助
    /// </summary>
    public static class ShellCommandHelper
    {
        /// <summary>
        /// 检测某个命令是否存在
        /// </summary>
        /// <param name="device"></param>
        /// <param name="cmd"></param>
        [Obsolete("检测方式非常危险", true)]
        public static void CommandExistsCheck(IDevice device, string cmd)
        {
            throw new NotImplementedException();
            //var result = new ShellCommand(device, cmd)
            //   .Execute();
            //if (result.ExitCode == (int)LinuxReturnCode.KeyHasExpired)
            //{
            //    throw new CommandNotFoundException(cmd);
            //}
        }
    }
}
