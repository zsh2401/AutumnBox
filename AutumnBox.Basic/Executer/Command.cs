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
    /// 封装命令
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
        public string FullCommand
        {
            get
            {
                return (serial != null) ?
                $" {serial.ToFullSerial()} {SpecificCommand}" : SpecificCommand;
            }
        }
        /// <summary>
        /// 具体命令
        /// </summary>
        private string SpecificCommand;
        private Serial serial;
        private Command() { }
        public static Command MakeForCmd(string command)
        {
            return new Command() { FileName = "cmd.exe", SpecificCommand = " /c " + command };
        }
        public static Command MakeForAdb(string command)
        {
            return new Command() { FileName = ConstData.FullAdbFileName, SpecificCommand = command };
        }
        public static Command MakeForAdb(Serial serial, string command)
        {
            return new Command() { FileName = ConstData.FullAdbFileName, serial = serial, SpecificCommand = command };
        }
        public static Command MakeForFastboot(string command)
        {
            return new Command() { FileName = ConstData.FullFastbootFileName, SpecificCommand = command };
        }
        public static Command MakeForFastboot(Serial serial, string command)
        {
            return new Command() { FileName = ConstData.FullFastbootFileName, serial = serial, SpecificCommand = command };
        }
        public static Command MakeForCustom(string fileName, string commanad)
        {
            return new Command() { FileName = fileName, SpecificCommand = commanad };
        }
    }
}