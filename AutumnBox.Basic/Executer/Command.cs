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
    using AutumnBox.Support.CstmDebug;

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
        /// 完全的命令
        /// </summary>
        public override string ToString()
        {
            return (serial != null) ?
                $" {serial.ToFullSerial()} {SpecificCommand}" : SpecificCommand;
        }
        /// <summary>
        /// 除去指定设备参数外的命令
        /// </summary>
        private string SpecificCommand;
        /// <summary>
        /// 指定的设备
        /// </summary>
        private Serial serial;
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
            return new Command() { FileName = "cmd.exe", SpecificCommand = " /c " + command };
        }
        /// <summary>
        /// 构建一个使用adb.exe运行的命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForAdb(string command)
        {
            return new Command() { FileName = ConstData.FullAdbFileName, SpecificCommand = command };
        }
        /// <summary>
        /// 构建一个使用adb.exe运行的命令,并且这个命令指定了设备
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForAdb(Serial _serial, string command)
        {
            return new Command() { FileName = ConstData.FullAdbFileName, serial = _serial, SpecificCommand = command };
        }
        /// <summary>
        /// 构建一个使用fastboot.exe运行的命令
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForFastboot(string command)
        {
            return new Command() { FileName = ConstData.FullFastbootFileName, SpecificCommand = command };
        }
        /// <summary>
        /// 构建一个使用fastboot.exe运行的命令,并且这个命令指定了设备
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForFastboot(Serial _serial, string command)
        {
            return new Command() { FileName = ConstData.FullFastbootFileName, serial = _serial, SpecificCommand = command };
        }
        /// <summary>
        /// 构建一个完全自定义的命令对象
        /// </summary>
        /// <param name="command"></param>
        /// <returns>命令对象</returns>
        public static Command MakeForCustom(string fileName, string commanad)
        {
            return new Command() { FileName = fileName, SpecificCommand = commanad };
        }
    }
}