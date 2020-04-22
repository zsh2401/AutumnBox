#nullable enable

namespace AutumnBox.Basic.Data
{
    /// <summary>
    /// 命令结果
    /// </summary>
    public sealed class CommandResult
    {
        /// <summary>
        /// 构造命令结果
        /// </summary>
        /// <param name="exitCode"></param>
        /// <param name="output"></param>
        public CommandResult(int exitCode, Output output)
        {
            ExitCode = exitCode;
            Output = output ?? throw new System.ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// 返回码
        /// </summary>
        public int ExitCode { get; }

        /// <summary>
        /// 输出
        /// </summary>
        public Output Output { get; }
    }
}
