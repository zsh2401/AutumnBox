using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Devices
{
    public class DevicesList : List<DeviceSimpleInfo>
    {
        public static DevicesList operator +(DevicesList left, DevicesList right)
        {
            right.ForEach((info) => { left.Add(info); });
            return left;
        }
        public static bool operator ==(DevicesList left, DevicesList right)
        {
            if (left.Count != right.Count) return false;//长度不同就是不同
            try
            {//不包含也是不同
                left.ForEach((i) => { if (!right.Contains(i)) { throw new Exception(); }; });
            }
            catch { return false; }
            return true;
        }
        public static bool operator !=(DevicesList left, DevicesList right)
        {
            return (!(left == right));
        }

    }
}
