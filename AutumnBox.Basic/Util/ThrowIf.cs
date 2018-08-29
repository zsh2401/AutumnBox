/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/29 0:39:14 (UTC +8:00)
** desc： ...
*************************************************/
using AutumnBox.Basic.Device;
using AutumnBox.Basic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Util
{
    public static class ThrowIf
    {
        public static void ThrowIfNotAlive(this IDevice device)
        {
            if (device.IsAlive == false)
            {
                throw new DeviceNotAliveException();
            }
        }
        public static void ThrowIfNull(this object any)
        {
            if (any == null)
            {
                throw new NullReferenceException();
            }
        }
        public static void ThrowIfNullArg(this object any)
        {
            if (any == null)
            {
                throw new ArgumentNullException();
            }
        }
        public static void IsNullArg(object any)
        {
            if (any == null)
            {
                throw new ArgumentNullException();
            }
        }
        public static void IsNull(object any)
        {
            if (any == null)
            {
                throw new NullReferenceException();
            }
        }
    }
}
