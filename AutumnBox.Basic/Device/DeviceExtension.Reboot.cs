/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/4 13:11:24 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    partial class DeviceExtension
    {
        /// <summary>
        /// 重启设备到recovery模式
        /// </summary>
        /// <param name="device"></param>
        public static void Reboot2Recovery(this IDevice device)
        {
            device.ThrowIfNotAlive();
            if (device.State != DeviceState.Fastboot)
            {
                device.Adb("reboot recovery").ThrowIfExitCodeNotEqualsZero();
            }
            else
            {
                throw new Exception("cant restart to fastboot when device on fastboot state");
            }
        }
        /// <summary>
        /// 重启设备到系统
        /// </summary>
        /// <param name="device"></param>
        public static void Reboot2System(this IDevice device)
        {
            device.ThrowIfNotAlive();
            if (device.State == DeviceState.Fastboot)
            {
                device.Fastboot("reboot").ThrowIfExitCodeNotEqualsZero();
            }
            else
            {
                device.Adb("reboot").ThrowIfExitCodeNotEqualsZero();
            }

        }
        /// <summary>
        /// 重启设备到fastboot模式
        /// </summary>
        /// <param name="device"></param>
        public static void Reboot2Fastboot(this IDevice device)
        {
            device.ThrowIfNotAlive();
            if (device.State == DeviceState.Fastboot)
            {
                device.Fastboot("reboot-bootloader").ThrowIfExitCodeNotEqualsZero();
            }
            else
            {
                device.Adb("reboot-bootloader").ThrowIfExitCodeNotEqualsZero();
            }
        }
        /// <summary>
        /// 重启设备到9008模式
        /// </summary>
        /// <param name="device"></param>
        public static void Reboot29008(this IDevice device)
        {
            device.ThrowIfNotAlive();
            if (device.State == DeviceState.Fastboot)
            {
                device.Fastboot("reboot-edl").ThrowIfExitCodeNotEqualsZero();
            }
            else
            {
                device.Adb("reboot-edl").ThrowIfExitCodeNotEqualsZero();
            }
        }
    }
}
