/* =============================================================================*\
*
* Filename: IDevicesChangedListener.cs
* Description: 
*
* Version: 1.0
* Created: 9/27/2017 02:46:14(UTC+8:00)
* Compiler: Visual Studio 2017
* 
* Author: zsh2401
* Company: I am free man
*
\* =============================================================================*/
namespace AutumnBox.Basic.Util.Interface
{
    using AutumnBox.Basic.Devices;
    public interface IDevicesChangedListener
    {
        void OnDevicesChanged(object sender, DevicesChangeEventArgs e);
    }
}
