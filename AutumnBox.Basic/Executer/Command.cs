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
    using AutumnBox.Basic.Devices;
    using AutumnBox.Basic.Util;
    /// <summary>
    /// 封装命令
    /// </summary>
    public class Command
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return ExecuteType == ExeType.Adb ? Paths.ADB_FILENAME : Paths.FASTBOOT_FILENAME;
            }
        }
        /// <summary>
        /// 完全的命令
        /// </summary>
        public string FullCommand
        {
            get
            {
                return isDesignatedDevice ?
                $" -s {Info?.Id} {SpecificCommand}" : SpecificCommand;
            }
        }
        /// <summary>
        /// 执行的类型
        /// </summary>
        public ExeType ExecuteType { get; private set; }
        /// <summary>
        /// 是否指定了设备
        /// </summary>
        bool isDesignatedDevice
        {
            get
            {
                return (Info != null);
            }
        }
        /// <summary>
        /// 具体命令
        /// </summary>
        string SpecificCommand;
        /// <summary>
        /// 设备信息
        /// </summary>
        public DeviceBasicInfo? Info { get; private set; }
        public Command(DeviceBasicInfo info, string command, ExeType executeType = ExeType.Adb) : this(command, executeType)
        {
            Info = info;
        }
        public Command(string id, string command, ExeType exeType = ExeType.Adb) :
            this(new DeviceBasicInfo() { Id = id, Status = DeviceStatus.UNKNOW }, command, exeType)
        { }
        public Command(string command, ExeType executeType = ExeType.Adb)
        {
            this.ExecuteType = executeType;
            SpecificCommand = command;
        }
        /// <summary>
        /// 获取一个adb指令
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Command MakeADB(string id, string command)
        {
            return new Command(id, command, ExeType.Adb);
        }
        /// <summary>
        /// 获取一个fastboot指令
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Command MakeFastboot(string id, string command)
        {
            return new Command(id, command, ExeType.Fastboot);
        }
        /// <summary>
        /// 获取一个adb指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Command MakeADB(string command)
        {
            return new Command(command, ExeType.Adb);
        }
        /// <summary>
        /// 获取一个adb指令
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static Command MakeFastboot(string command)
        {
            return new Command(command, ExeType.Fastboot);
        }
        /// <summary>
        /// 获取完整的指令(无文件名)
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FullCommand;
        }
    }
}