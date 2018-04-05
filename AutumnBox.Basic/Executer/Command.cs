/* =============================================================================*\
*
* Filename: Command.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 00:19:21(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Executer
{
    using AutumnBox.Basic.Device;
    using AutumnBox.Basic.Util;

    /// <summary>
    /// 封装命令的对象
    /// </summary>
    public sealed class Command
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// 参数
        /// </summary>
        public string Args { get; private set; }
        /// <summary>
        /// 参数数组
        /// </summary>
        public string[] ArgArray { get { return Args.Split(' '); } }

        /// <summary>
        /// 仅限使用方法进行构建
        /// </summary>
        private Command() { }
        /// <summary>
        /// 构建一个使用cmd.exe运行的命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForCmd(string command)
        {
            return new Command()
            {
                FileName = "cmd.exe",
                Args = $"/c {command}",
            };
        }
        /// <summary>
        /// 构建一个使用adb.exe运行的命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForAdb(string command)
        {
            return new Command()
            {
                FileName = AdbConstants.FullAdbFileName,
                Args = command
            };
        }
        /// <summary>
        /// 构建一个使用adb.exe运行的命令,并且这个命令指定了设备
        /// </summary>
        /// <param name="_serial">设备序列号</param>
        /// <param name="command">命令</param>
        /// <returns>命令对象</returns>
        public static Command MakeForAdb(DeviceSerialNumber _serial, string command)
        {
            return new Command()
            {
                FileName = AdbConstants.FullAdbFileName,
                Args = $"-s {_serial} {command}",
            };
        }
        /// <summary>
        /// 构建一个使用fastboot.exe运行的命令
        /// </summary>
        /// <param name="command">命令</param>
        /// <returns>命令对象</returns>
        public static Command MakeForFastboot(string command)
        {
            return new Command()
            {
                FileName = AdbConstants.FullFastbootFileName,
                Args = command
            };
        }
        /// <summary>
        /// 构建一个使用fastboot.exe运行的命令,并且这个命令指定了设备
        /// </summary>
        /// <param name="_serial">设备序列号</param>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForFastboot(DeviceSerialNumber _serial, string command)
        {
            return new Command()
            {
                FileName = AdbConstants.FullFastbootFileName,
                Args = $"-s {_serial} {command}"
            };
        }
        /// <summary>
        /// 构建一个完全自定义的命令对象
        /// </summary>
        /// <param name="fileName">程序文件名</param>
        /// <param name="args">参数</param>
        /// <returns>命令对象</returns>
        public static Command MakeForCustom(string fileName, string args)
        {
            return new Command()
            {
                FileName = fileName,
                Args = args
            };
        }
        /// <summary>
        /// 构建一个完全自定义的命令对象
        /// </summary>
        /// <param name="fileName">程序文件名</param>
        /// <param name="args">参数</param>
        /// <returns>命令对象</returns>
        public static Command MakeForCustom(string fileName, params string[] args)
        {
            return new Command()
            {
                FileName = fileName,
                Args = string.Join(" ", args)
            };
        }
    }
}