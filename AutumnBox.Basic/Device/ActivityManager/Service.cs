/*************************************************
** auth： zsh2401@163.com
** date:  2018/1/30 17:54:00 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Executer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.ActivityManager
{
    /// <summary>
    /// 服务管理器
    /// </summary>
    public static class Service
    {
        /// <summary>
        /// 启动某个服务
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="pkgName">包名</param>
        /// <param name="className">服务类名</param>
        /// <returns></returns>
        public static AdvanceOutput RunService(DeviceSerial device,
            string pkgName, 
            string className) {
            return ActivityManagerShared.Executer.QuicklyShell(device,$"am startservice {pkgName}/.{className}");
        }
    }
}
