/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 12:47:58 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.Management.OS
{
    public class DpiModifier : DependOnDeviceObject, IDpiModifier
    {
        public DpiModifier(IDevice device) : base(device)
        {
        }

        public int GetSourceDpi()
        {
            int? result = new DeviceHardwareInfoGetter(new DeviceSerialNumber(Device.SerialNumber)).GetDpi();
            if (result == null)
            {
                throw new Exception("get dpi failed");
            }
            return (int)result;
        }

        public void SetDpi(int dpi)
        {
            Device.Shell($"wm density {dpi}")
                .ThrowIfExitCodeNotEqualsZero();
        }
    }
}
