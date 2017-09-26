using AutumnBox.Basic.Devices;
using AutumnBox.Basic.Util;

namespace AutumnBox.Basic.Executer
{
    public class Command
    {
        public string FileName { get {
                return ExecuteType == ExeType.Adb ? Paths.ADB_FILENAME : Paths.FASTBOOT_FILENAME;
            } }
        public string FullCommand { get {
                return isDesignatedDevice?
                $" -s {Info.Id} {SpecificCommand}" : SpecificCommand;
            } }
        public ExeType ExecuteType { get; private set; }
        bool isDesignatedDevice = false;
        string SpecificCommand;
        public DeviceSimpleInfo Info { get; private set; }
        public Command(DeviceSimpleInfo info, string command,ExeType executeType = ExeType.Adb):this(command,executeType)
        {
            isDesignatedDevice = true;
            Info = info;
        }
        public Command(string command, ExeType executeType = ExeType.Adb)
        {
            this.ExecuteType = executeType;
            SpecificCommand = command;
        }
        public override string ToString()
        {
            return FullCommand;
        }
    }
}