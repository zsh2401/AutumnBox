#nullable enable
using AutumnBox.Basic.Device;
using AutumnBox.Basic.ManagedAdb.CommandDriven;

namespace AutumnBox.Basic.Calling
{
    /// <summary>
    /// 命令事务管理器拓展函数
    /// </summary>
    public static class CPMExtension
    {
        public ICommandProcedure OpenShellCommand(this ICommandProcedureManager cpm,
            IDevice device, params string[] sh)
        { }
        public ICommandProcedure OpenADBCommand(this ICommandProcedureManager cpm,
            IDevice? device, params string[] args)
        { }
        public ICommandProcedure OpenFastbootCommand(this ICommandProcedureManager cpm,
            IDevice? device, params string[] args)
        { }
        public ICommandProcedure OpenCMDCommand(this ICommandProcedureManager cpm,
            IDevice? device, params string[] args)
        {
        }
    }
}
