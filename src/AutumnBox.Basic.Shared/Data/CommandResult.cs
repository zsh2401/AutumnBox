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
        /// 指示其是否是被强制中断的
        /// </summary>
        public bool Cancelled { get; set; } = false;

        /// <summary>
        /// 输出
        /// </summary>
        public Output Output { get; }
    }
}
