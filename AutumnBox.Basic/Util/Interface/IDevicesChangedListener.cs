namespace AutumnBox.Basic.Util.Interface
{
    using AutumnBox.Basic.Devices;
    public interface IDevicesChangedListener
    {
        void OnDevicesChanged(object sender, DevicesChangeEventArgs e);
    }
}
