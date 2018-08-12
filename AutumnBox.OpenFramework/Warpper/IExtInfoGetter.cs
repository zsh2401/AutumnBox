using AutumnBox.Basic.Device;
using AutumnBox.OpenFramework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.OpenFramework.Warpper
{
    /// <summary>
    /// 拓展模块特性获取器
    /// </summary>
    public interface IExtInfoGetter
    {
        string Name { get; }
        string Desc { get; }
        string FormatedDesc { get; }
        byte[] Icon { get; }
        DeviceState RequiredDeviceStates { get; }
        int MinApi { get; }
        int TargetApi { get; }
        Type ExtType { get; }
        bool RunAsAdmin { get; }
        Version Version { get; }
        void Reload();
    }
}
