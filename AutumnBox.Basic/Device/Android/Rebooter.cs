/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 4:39:14 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device.OS
{
    /// <summary>
    /// 重启器
    /// </summary>
    public class Rebooter
    {
        /// <summary>
        /// 异步重启(如果ADB或手机卡到爆,那么这个函数就有作用了)
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="option"></param>
        /// <param name="callback"></param>
        public void RebootAsync(DeviceBasicInfo dev, RebootOptions option = RebootOptions.System, Action callback = null)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 重启手机
        /// </summary>
        /// <param name="dev"></param>
        /// <param name="option"></param>
        public void Reboot(DeviceBasicInfo dev, RebootOptions option)
        {
            throw new NotImplementedException();
        }
    }
}
