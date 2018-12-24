/*************************************************
** auth： zsh2401@163.com
** date:  2018/9/1 18:24:52 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using System.Collections.Generic;
using System.Linq;

namespace AutumnBox.Basic.MultipleDevices
{
    /// <summary>
    /// 设备数组拖着赞
    /// </summary>
    public static class DevicesArrayExtension
    {
        /// <summary>
        /// 设备数组对比
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool DevicesEquals(this IEnumerable<IDevice> left, IEnumerable<IDevice> right)
        {
            if (left.Count() != right.Count()) return false;
            foreach (IDevice info in left)
            {
                if (!right.Contains(info)) return false;
            }
            return true;
        }
    }
}
