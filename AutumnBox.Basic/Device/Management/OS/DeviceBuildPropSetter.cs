/********************************************************************************
** auth： zsh2401@163.com
** date： 2018/1/12 23:45:05
** filename: DeviceBuildPropSetter.cs
** compiler: Visual Studio 2017
** desc： ...
*********************************************************************************/
using System;

namespace AutumnBox.Basic.Device.Management.OS
{
    [Obsolete("由于安卓碎片化原因,此类不保证可以正常运行,请自行实现相关功能", true)]
#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释
    public class DeviceBuildPropSetter : DeviceCommander
    {
        public DeviceBuildPropSetter(IDevice device) : base(device)
        {
            if (!device.HaveSU())
            {
                throw new Exceptions.DeviceHasNoSuException();
            }
            Manager = new NoSuCheckManager(device);
        }

        private class NoSuCheckManager : DeviceBuildPropManager
        {
            public NoSuCheckManager(IDevice device) : base(device)
            {
            }
            protected override void SettingCheck()
            {
            }
        }
        private DeviceBuildPropManager Manager { get; set; }
        public void Set(string key, string value)
        {
            Manager.CmdStation = this.CmdStation;
            Manager.Set(key, value);
        }
    }
}
