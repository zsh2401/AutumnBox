/*************************************************
** auth： zsh2401@163.com
** date:  2018/4/17 18:29:11 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;

namespace AutumnBox.GUI.UI.FuncPanels.PoweronFuncsUx
{
    public interface IPoweronFuncsUX
    {
        void ActivateBrevent(DeviceBasicInfo targetDevice);
        void ActivateIceBox(DeviceBasicInfo targetDevice);
        void ActivateAirForzen(DeviceBasicInfo targetDevice);
        void ActivateShizukuManager(DeviceBasicInfo targetDevice);
        void ActivateIsland(DeviceBasicInfo targetDevice);
        void ActivateGeekMemoryCleaner(DeviceBasicInfo targetDevice);
        void ActivateStopapp(DeviceBasicInfo targetDevice);
        void ActivateBlackHole(DeviceBasicInfo targetDevice);
        void ActivateAnzenbokusu(DeviceBasicInfo targetDevice);
        void ActivateAnzenbokusuFake(DeviceBasicInfo targetDevice);
        void ActivateFreezeYou(DeviceBasicInfo targetDevice);
        void ActivateGreenifyAggressiveDoze(DeviceBasicInfo targetDevice);
        void ActivateUsersir(DeviceBasicInfo targetDevice);
    }
}
