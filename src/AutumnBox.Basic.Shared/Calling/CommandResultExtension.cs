using AutumnBox.Basic.Data;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// ICommandResult的拓展方法
    /// </summary>
    public static class CommandResultExtension
    {
        private const int STATUS_OK = 0;
        /// <summary>
        /// 当有返回码不为0时抛出异常
        /// </summary>
        /// <param name="result"></param>
        /// <exception cref="CommandErrorException">返回码不为0时抛出该异常</exception>
        /// <returns>如果未抛出异常,则返回传入的结果以提供链式调用</returns>
        public static CommandResult ThrowIfError(this CommandResult result)
        {
            if (result.ExitCode != STATUS_OK)
            {
                throw new CommandErrorException(result.Output, result.ExitCode);
            }
            return result;
        }
    }
}
