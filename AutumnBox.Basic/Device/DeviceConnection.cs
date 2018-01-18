using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Device
{
    public sealed class DeviceConnection
    {
        public Serial Serial { get { return DevInfo.Serial; } }
        public DeviceBasicInfo DevInfo { get; private set; }
        public DeviceConnection()
        {
            Reset();
        }
        public void Reset(DeviceBasicInfo basicInfo)
        {
            this.DevInfo = basicInfo;
        }
        public void Reset()
        {
            this.DevInfo = new DeviceBasicInfo() { State = DeviceState.None };
        }
    }
}
