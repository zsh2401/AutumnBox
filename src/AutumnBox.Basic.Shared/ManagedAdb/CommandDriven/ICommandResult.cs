#nullable enable
using AutumnBox.Basic.Data;

namespace AutumnBox.Basic.ManagedAdb.CommandDriven
{
    /// <summary>
    /// 命令执行结果
    /// </summary>
    public interface ICommandResult
    {
        /// <summary>
        /// 返回码
        /// </summary>
        int? ExitCode { get; }
        /// <summary>
        /// 标准输出/错误内容
        /// </summary>
        Output Output { get; }
    }
}
