/*************************************************
** auth： zsh2401@163.com
** date:  2018/8/30 4:54:12 (UTC +8:00)
** desc： ...
*************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutumnBox.Basic.Device
{
    public abstract class DeviceBase : IDevice
    {
        public string SerialNumber { get;internal protected set; }

        public string Product { get; internal protected set; }

        public string Model { get; internal protected set; }

        public string TransportId { get; internal protected set; }

        public DeviceState State { get; internal protected set; }

        public bool IsAlive { get; internal protected set; }

        public bool Equals(IDevice other)
        {
            return other != null && other.SerialNumber == this.SerialNumber;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IDevice);
        }

        public override int GetHashCode()
        {
            var hashCode = 927976858;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(SerialNumber);
            return hashCode;
        }

        public static bool operator ==(DeviceBase base1, DeviceBase base2)
        {
            return EqualityComparer<DeviceBase>.Default.Equals(base1, base2);
        }

        public static bool operator !=(DeviceBase base1, DeviceBase base2)
        {
            return !(base1 == base2);
        }
    }
}
