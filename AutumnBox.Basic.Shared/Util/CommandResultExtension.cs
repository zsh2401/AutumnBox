using AutumnBox.Basic.Calling;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Util
{
    /// <summary>
    /// 一些ICommandResult的拓展方法
    /// </summary>
    public static class CommandResultExtension
    {
        /// <summary>
        /// 如果ExitCode不等于0,抛出异常
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="CommandFaultException"></exception>
        /// <returns></returns>
        public static ICommandResult ThrowIfError(this ICommandResult result)
        {
            if (result.ExitCode != 0)
            {
                throw new CommandFaultException(result);
            }
            else
            {
                return result;
            }
        }
        /// <summary>
        /// 转化为一段可视字符串
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static string ToStdString(this ICommandResult result)
        {
            return $"exitcode:{result.ExitCode}{Environment.NewLine}{result.Output}";
        }
    }
}
