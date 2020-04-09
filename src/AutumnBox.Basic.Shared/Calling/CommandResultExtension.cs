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
        /// <exception cref="Exceptions.CommandErrorException">返回码不为0时抛出该异常</exception>
        /// <returns></returns>
        public static ICommandResult ThrowIfError(this ICommandResult result)
        {
            if (result.ExitCode != STATUS_OK)
            {
                throw new Exceptions.CommandErrorException(result.Output, result.ExitCode);
            }
            return result;
        }
    }
}
