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
        /// <summary>
        /// 打开一个向shell执行的命令
        /// </summary>
        /// <param name="cpm"></param>
        /// <param name="device"></param>
        /// <param name="sh"></param>
        /// <returns></returns>
        public static ICommandProcedure OpenShellCommand(this ICommandProcedureManager cpm,
            IDevice device, params string[] sh)
        {
            return cpm.OpenADBCommand(device, $"shell {string.Join(" ",sh)}");
        }

        /// <summary>
        /// 打开一条ADB命令
        /// </summary>
        /// <param name="cpm"></param>
        /// <param name="device">指定设备,传入NULL则不指定</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandProcedure OpenADBCommand(this ICommandProcedureManager cpm,
            IDevice? device, params string[] args)
        {
            var deviceArg = device != null ? $"-s {device.SerialNumber}" : "";
            return cpm.OpenCommand("adb.exe", $"{deviceArg}", string.Join(" ", args));
        }

        /// <summary>
        /// 打开一条Fastboot命令
        /// </summary>
        /// <param name="cpm"></param>
        /// <param name="device">传入NULL则不指定设备</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandProcedure OpenFastbootCommand(this ICommandProcedureManager cpm,
            IDevice? device, params string[] args)
        {
            var deviceArg = device != null ? $"-s {device.SerialNumber}" : "";
            return cpm.OpenCommand("fastboot.exe", $"{deviceArg}", string.Join(" ", args));
        }

        /// <summary>
        /// 打开一条系统命令行命令
        /// </summary>
        /// <param name="cpm"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static ICommandProcedure OpenCMDCommand(this ICommandProcedureManager cpm, params string[] args)
        {
#if CROSS_PLATFORM
            string fileName = System.Environment.OSVersion.Platform == System.PlatformID.Win32NT ? "cmd.exe" : "/bin/bash";
#else
            string fileName = "cmd.exe";
#endif
            return cpm.OpenCommand(fileName, "/c", string.Join(" ", args));
        }
    }
}
