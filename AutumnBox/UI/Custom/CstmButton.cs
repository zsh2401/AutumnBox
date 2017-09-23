using AutumnBox.Basic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AutumnBox
{
    public class CstmButton:Button
    {
        public DeviceStatus NeedStatus { get{ return (DeviceStatus)GetValue(NeedStatusProperty); } }
        public static readonly
            DependencyProperty NeedStatusProperty = DependencyProperty.Register("NeedStatus", typeof(DeviceStatus), typeof(CstmButton),new PropertyMetadata(null));
        public void Set(DeviceStatus status) {

        }
    }
}
