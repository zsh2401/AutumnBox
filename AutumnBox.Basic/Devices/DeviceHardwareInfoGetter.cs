/********************************************************************************
** auth： zsh2401@163.com
** date： 2017/12/25 22:23:30
** filename: DeviceHardwareInfoGetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using AutumnBox.Basic.Connection;
using AutumnBox.Basic.Executer;

namespace AutumnBox.Basic.Devices
{
    public class DeviceHardwareInfoGetter
    {
        private readonly AndroidShell shell;
        public DeviceHardwareInfoGetter(Serial deviceSerial)
        {
            shell = new AndroidShell(deviceSerial);
        }

    }
}
