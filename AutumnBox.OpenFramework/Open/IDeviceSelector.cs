using AutumnBox.Basic.Device;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.OpenFramework.Open
{
    public interface IDeviceSelector
    {
        IDevice CurrentSelection { get; }
    }
}
