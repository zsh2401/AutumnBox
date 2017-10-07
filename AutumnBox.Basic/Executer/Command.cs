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
        public string FileName
        {
            get
            {
                return ExecuteType == ExeType.Adb ? Paths.ADB_FILENAME : Paths.FASTBOOT_FILENAME;
            }
        }
        public string FullCommand
        {
            get
            {
                return isDesignatedDevice ?
                $" -s {Info.Id} {SpecificCommand}" : SpecificCommand;
            }
        }
        public ExeType ExecuteType { get; private set; }
        bool isDesignatedDevice = false;
        string SpecificCommand;
        public DeviceSimpleInfo Info { get; private set; }
        public Command(DeviceSimpleInfo info, string command, ExeType executeType = ExeType.Adb) : this(command, executeType)
        {
            isDesignatedDevice = true;
            Info = info;
        }
        public Command(string id, string command, ExeType exeType = ExeType.Adb) :
            this(new DeviceSimpleInfo() { Id = id, Status = DeviceStatus.UNKNOW }, command, exeType)
        { }
        public Command(string command, ExeType executeType = ExeType.Adb)
        {
            this.ExecuteType = executeType;
            SpecificCommand = command;
        }
        public static Command MakeADB(string id,string command) {
            return new Command(id, command, ExeType.Adb);
        }
        public static Command MakeFastboot(string id, string command) {
            return new Command(id,command, ExeType.Fastboot);
        }
        public static Command MakeADB(string command) {
            return new Command(command, ExeType.Adb);
        }
        public static Command MakeFastboot(string command) {
            return new Command(command, ExeType.Fastboot);
        }
        public override string ToString()
        {
            return FullCommand;
        }
    }
}