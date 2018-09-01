/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/27 5:56:42 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Data;
using System;

namespace AutumnBox.Basic.Exceptions
{

    /// <summary>
    /// 命令执行错误
    /// </summary>
    [Serializable]
    public class ExcutionException : Exception
    {
        /// <summary>
        /// 具体的执行结果
        /// </summary>
        public Output ExecuteResult { get; private set; }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="result"></param>
        public ExcutionException(Output result)
        {
            this.ExecuteResult = result;
        }
    }
}
