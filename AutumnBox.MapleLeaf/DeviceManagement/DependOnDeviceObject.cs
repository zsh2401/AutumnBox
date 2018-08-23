/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/22 19:36:16 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Text;

namespace AutumnBox.MapleLeaf.DeviceManagement
{
    public abstract class DependOnDeviceObject
    {
        public IDevice TargetDevice { get; set; }
        public DependOnDeviceObject(IDevice device)
        {
            TargetDevice = device;
        }
    }
}
